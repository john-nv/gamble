using OkVip.Gamble.Localization;
using Volo.Abp.Application.Services;

namespace OkVip.Gamble;

/* Inherit your application services from this class.
 */

public abstract class GambleAppService : ApplicationService
{
	protected GambleAppService()
	{
		LocalizationResource = typeof(GambleResource);
	}
}