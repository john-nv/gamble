using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OkVip.Gamble.Accounts;
using OkVip.Gamble.IdentityUsers;
using OkVip.Gamble.Localization;
using OkVip.Gamble.Permissions;
using OkVip.Gamble.Settings;
using OkVip.Gamble.Transactions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Identity;
using Volo.Abp.Settings;
using Volo.Abp.Users;

namespace OkVip.Gamble.Tickets
{
    [Authorize]
    public class TicketAppService : CrudAppService<Ticket, TicketOutputDto, Guid, TicketGetListInputDto, TicketCreateOrUpdateDto>, ITicketAppService
    {
        public ICurrentUser CurrentUser { get; set; }

        public IAbpDistributedLock DistributedLock { get; set; }

        public IIdentityUserRepository IdentityUserRepository { get; set; }

        public IRepository<Transaction, Guid> TransactionRepository { get; set; }
        public IRepository<Account, Guid> AccountRepository { get; set; }

        public ISettingProvider SettingProvider { get; set; }

        public ILocalEventBus LocalEventBus { get; set; }

        public TicketAppService(IRepository<Ticket, Guid> repository) : base(repository)
        {
            LocalizationResource = typeof(GambleResource);
        }

        public virtual async Task<TicketOutputDto> CreateWithdrawAsync(WithdrawTicketCreateOrUpdateDto input)
        {
            await using (var handle = await DistributedLock.TryAcquireAsync($"OkVip.Gamble.Tickets.CreateWithdrawAsync.{CurrentUser.Id}"))
            {
                if (handle != null)
                {
                    var idenittyUser = await IdentityUserRepository.GetAsync(CurrentUser.Id.Value);

                    if (idenittyUser.GetWithdrawPassword() != input.WithdrawPassword)
                    {
                        throw new UserFriendlyException("Mật khẩu rút tiền không đúng");
                    }

                    if (!idenittyUser.IsActive)
                    {
                        throw new UserFriendlyException("Người dùng đang bị vô hiệu, vui lòng liên hệ CSKH");
                    }

                    if (!(idenittyUser.GetAccountBalance() >= input.Amount))
                    {
                        throw new UserFriendlyException("Số dư không đủ");
                    }

                    if (!await AccountRepository.AnyAsync(e => e.CreatorId == CurrentUser.Id))
                    {
                        throw new UserFriendlyException("Bạn chưa liên kết với ngân hàng");
                    }

                    if (await Repository.AnyAsync(e => e.Status == TicketStatusConsts.New && e.CreatorId == CurrentUser.Id.Value))
                    {
                        throw new UserFriendlyException("Bạn đã tạo một yêu cầu trước đó, vui lòng chờ đến khi yêu cầu được xử lý xong");
                    }

                    var ticket = new Ticket
                    {
                        Title = $"Yêu cầu rút tiền - {CurrentUser.UserName}",
                        Status = TicketStatusConsts.New,
                        CreatorUsername = CurrentUser.UserName,
                        CreatorName = CurrentUser.Name,
                        Amount = input.Amount.Value,
                        TicketType = TicketTypeConsts.Withdraw,
                        Transaction = new Transaction
                        {
                            Amount = input.Amount.Value * -1,
                            Status = TransactionStatusConsts.Success,
                            Type = TransactionTypeConsts.Withdraw,
                            UserId = CurrentUser.Id.Value,
                        },
                        TicketLogs = new List<TicketLog>
                        {
                            new TicketLog
                            {
                                Status = TicketStatusConsts.New,
                            }
                        }
                    };

                    await Repository.InsertAsync(ticket, true);
                    return await MapToGetOutputDtoAsync(ticket);
                }
                else
                {
                    throw new UserFriendlyException("Đang xử lý, vui lòng thử lại sau");
                }
            }
        }

        [Authorize(GamblePermissions.Ticket.Create)]
        public override async Task<TicketOutputDto> CreateAsync(TicketCreateOrUpdateDto input)
        {
            var ticket = new Ticket
            {
                Status = TicketStatusConsts.New,
                CreatorUsername = CurrentUser.UserName,
                CreatorName = CurrentUser.Name,
                TicketLogs = new List<TicketLog>
                {
                    new TicketLog
                    {
                        Status = TicketStatusConsts.New
                    }
                }
            };

            await Repository.InsertAsync(ObjectMapper.Map(input, ticket), true);

            return await MapToGetOutputDtoAsync(ticket);
        }

        [Authorize(GamblePermissions.Ticket.Update)]
        public override Task<TicketOutputDto> UpdateAsync(Guid id, TicketCreateOrUpdateDto input)
        {
            return base.UpdateAsync(id, input);
        }

        [Authorize(GamblePermissions.Ticket.ApproveOrReject)]
        [HttpPost]
        public virtual async Task<TicketOutputDto> ApproveAsync(Guid id)
        {
            await using (var handle = await DistributedLock.TryAcquireAsync($"Ticket.{id}"))
            {
                if (handle != null)
                {
                    var entity = await Repository.GetAsync(id);

                    if (entity.IsApproved())
                    {
                        throw new UserFriendlyException(message: "Ticket đã được phê duyệt");
                    }

                    entity.Status = TicketStatusConsts.Approved;
                    entity.ApprovedTime = Clock.Now;

                    entity.TicketLogs.Add(new TicketLog
                    {
                        Status = TicketStatusConsts.Approved,
                    });

                    await Repository.UpdateAsync(entity, true);
                    return await MapToGetOutputDtoAsync(entity);
                }
                else
                {
                    throw new UserFriendlyException(message: "Ticket đang được xử lý");
                }
            }
        }

        [Authorize(GamblePermissions.Ticket.ApproveOrReject)]
        public virtual async Task<TicketOutputDto> RejectAsync(Guid id, TicketRejectInputDto input)
        {
            await using (var handle = await DistributedLock.TryAcquireAsync($"Ticket.{id}"))
            {
                if (handle != null)
                {
                    var entity = await Repository.GetAsync(id);

                    if (entity.IsApproved())
                    {
                        throw new UserFriendlyException(message: "Ticket đã được phê duyệt");
                    }

                    entity.Status = TicketStatusConsts.Rejected;
                    entity.ApprovedTime = Clock.Now;

                    entity.TicketLogs.Add(new TicketLog
                    {
                        Status = TicketStatusConsts.Rejected,
                        Note = input.Note,
                    });

                    if (entity.Transaction != null)
                    {
                        entity.Transaction.IsDeleted = true;
                    }

                    await Repository.UpdateAsync(entity, true);
                    await LocalEventBus.PublishAsync(new EntityChangedEventData<Transaction>(entity.Transaction));
                    return await MapToGetOutputDtoAsync(entity);
                }
                else
                {
                    throw new UserFriendlyException(message: "Ticket đang được xử lý");
                }
            }
        }

        [Authorize(GamblePermissions.Ticket.Delete)]
        public override Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        protected override async Task<IQueryable<Ticket>> CreateFilteredQueryAsync(TicketGetListInputDto input)
        {
            var queryable = await base.CreateFilteredQueryAsync(input);
            return queryable.WhereIf(!string.IsNullOrEmpty(input.Status), e => e.Status == input.Status)
                            .WhereIf(!string.IsNullOrEmpty(input.Status), e => e.CreatorUsername == input.Username)
                            .WhereIf(input.From.HasValue, e => e.CreationTime.Date >= input.From.Value.Date)
                            .WhereIf(input.To.HasValue, e => e.CreationTime.Date < input.To.Value.AddDays(1).Date);
        }

        protected override async Task<TicketOutputDto> MapToGetOutputDtoAsync(Ticket entity)
        {
            var dto = await base.MapToGetOutputDtoAsync(entity);

            if (entity.TicketType == TicketTypeConsts.Withdraw)
            {
                var amountInVndUnit = await SettingProvider.GetAsync(GambleSettings.AmountInVndUnit, 0m);

                dto.AmountInVnd = entity.Amount * amountInVndUnit;
                dto.AmountInVndText = $"{Convert.ToInt64(dto.AmountInVnd).ToWords(new CultureInfo("vi"))} đồng".ToUpper();
            }

            return dto;
        }

        protected override async Task<TicketOutputDto> MapToGetListOutputDtoAsync(Ticket entity)
        {
            return await MapToGetOutputDtoAsync(entity);
        }
    }
}