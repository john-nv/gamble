using OkVip.Gamble.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace OkVip.Gamble.Web.Pages;

public abstract class GamblePageModel : AbpPageModel
{
	protected GamblePageModel()
	{
		LocalizationResourceType = typeof(GambleResource);
	}
}