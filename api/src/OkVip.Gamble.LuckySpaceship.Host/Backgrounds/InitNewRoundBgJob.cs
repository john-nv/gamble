using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using OkVip.Gamble.Hubs;
using OkVip.Gamble.Rounds;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace OkVip.Gamble.Backgrounds
{
	public class InitNewRoundBgJob : AsyncBackgroundJob<InitNewRoundArgs>, ITransientDependency
	{
		public IRoundRepository RoundRepository { get; set; }

		public IHubContext<LuckySpaceshipHub> HubContext { get; set; }

		public override async Task ExecuteAsync(InitNewRoundArgs args)
		{
			await RoundRepository.InitNewRoundAsync(args.GameId);
			await HubContext.Clients.Group(args.GameId.ToString()).SendAsync("RenewGame");

			Logger.LogInformation($"ROUND OF GAME LUCKY SPACESHIP IS INITIALED ");
		}
	}
}