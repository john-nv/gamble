using OkVip.Gamble.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace OkVip.Gamble.Controllers;

/* Inherit your controllers from this class.
 */

public abstract class GambleController : AbpControllerBase
{
	protected GambleController()
	{
		LocalizationResource = typeof(GambleResource);
	}


}