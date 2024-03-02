using Microsoft.Extensions.Configuration;
using OkVip.Gamble.IdentityUsers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace OkVip.Gamble.Tokens
{
    public class TokenAppService : ApplicationService, ITokenAppService
    {
        public IHttpClientFactory HttpClientFactory { get; set; }

        public IIdentityUserRepository IdentityUserRepository { get; set; }

        public IConfiguration Configuration { get; set; }

        protected virtual HttpClient HttpClient
            => HttpClientFactory.CreateClient(Guid.NewGuid().ToString("N"));

        public virtual async Task<TokenOutputDto> CreateAsync(TokenInputDto input)
        {
            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, string.Format("{0}/connect/token", Configuration["IdentityClients:Default:Authority"]));
            var httpContent = new FormUrlEncodedContent(
                new[]
                {
                    new KeyValuePair<string, string>("grant_type",Configuration["IdentityClients:Default:GrantType"]!),
                    new KeyValuePair<string, string>("client_id", Configuration["IdentityClients:Default:ClientId"]!),
                    new KeyValuePair<string, string>("client_secret", Configuration["IdentityClients:Default:ClientSecret"]!),
                    new KeyValuePair<string, string>("username", input.Username),
                    new KeyValuePair<string, string>("password", input.Password),
                    new KeyValuePair<string, string>("scope", Configuration["IdentityClients:Default:Scope"]!),
                });

            tokenRequest.Content = httpContent;

            var response = await HttpClient.SendAsync(tokenRequest);
            var result = JsonNode.Parse(await response.Content.ReadAsStringAsync());
            var dto = new TokenOutputDto
            {
                AccessToken = result["access_token"]?.ToString(),
            };

            if (string.IsNullOrEmpty(dto.AccessToken))
                throw new UserFriendlyException(
                    message: "Đăng nhập không thành công",
                    details: "Vui lòng kiểm tra thông tin tài khoản hoặc mật khẩu");

            var identity = await IdentityUserRepository.FindByNormalizedUserNameAsync(input.Username.ToUpper());
            dto.Profile = ObjectMapper.Map<IdentityUser, IdentityUserCustomDto>(identity);

            return dto;
        }
    }
}