using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace OkVip.Gamble.Rounds
{
	public interface IRoundRepository : IRepository<Round, Guid>
	{
		Task<Round> InitNewRoundAsync(Guid gameId);
	}
}
