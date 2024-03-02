using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace OkVip.Gamble.Tokens
{
	public interface ITokenAppService : IApplicationService
	{
		Task<TokenOutputDto> CreateAsync(TokenInputDto input);
	}
}
