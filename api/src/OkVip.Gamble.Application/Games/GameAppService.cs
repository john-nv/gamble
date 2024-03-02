using Microsoft.AspNetCore.Authorization;
using OkVip.Gamble.Localization;
using OkVip.Gamble.Permissions;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OkVip.Gamble.Games
{
	[Authorize(GamblePermissions.Game.Default)]
	public class GameAppService : CrudAppService<Game, GameOutputDto, Guid, GameGetListInputDto, GameCreateOrUpdateDto>, IGameAppService
	{
		public GameAppService(IRepository<Game, Guid> repository) : base(repository)
		{
			LocalizationResource = typeof(GambleResource);
		}

		[Authorize(GamblePermissions.Game.Create)]
		public override Task<GameOutputDto> CreateAsync(GameCreateOrUpdateDto input)
		{
			return base.CreateAsync(input);
		}

		[Authorize(GamblePermissions.Game.Update)]
		public override Task<GameOutputDto> UpdateAsync(Guid id, GameCreateOrUpdateDto input)
		{
			return base.UpdateAsync(id, input);
		}

		[Authorize(GamblePermissions.Game.Delete)]
		public override Task DeleteAsync(Guid id)
		{
			return base.DeleteAsync(id);
		}
	}
}