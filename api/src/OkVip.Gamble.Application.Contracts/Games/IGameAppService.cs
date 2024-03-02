using System;
using Volo.Abp.Application.Services;

namespace OkVip.Gamble.Games
{
	public interface IGameAppService : ICrudAppService<GameOutputDto, Guid, GameGetListInputDto, GameCreateOrUpdateDto>
	{

	}
}
