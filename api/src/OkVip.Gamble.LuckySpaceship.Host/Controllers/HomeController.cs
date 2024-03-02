using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace OkVip.Gamble.Controllers;

public class HomeController : AbpController
{
	public ActionResult Index()
	{
		return Redirect("~/swagger");
	}
}