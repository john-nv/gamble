using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.SignalR;

namespace OkVip.Gamble.Hubs
{
	[HubRoute("/lucky-spaceship")]
	public class LuckySpaceshipHub : AbpHub
	{
		public Task JoinGame(Guid gameId)
		{
			return Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
		}
	}
}
