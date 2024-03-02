using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace OkVip.Gamble.Rounds
{
	public interface IRoundAppService : IApplicationService
	{
		Task<IList<PublicRoundHistoryOutputDto>> GetHistoriesAsync(Guid gameId, int? limit = 100);

		Task<PublicRoundOutputDto> GetPreviousRoundAsync(Guid gameId);

		Task<PublicRoundDetailOutputDto> GetAsync(Guid gameId, Guid roundId);

		Task<PublicRoundDetailOutputDto> CreateAsync(Guid gameId, Guid roundId, RoundDetailCreateDto input);
	}
}