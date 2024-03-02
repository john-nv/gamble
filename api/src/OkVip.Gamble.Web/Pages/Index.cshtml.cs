using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;

namespace OkVip.Gamble.Web.Pages;

public class IndexModel : GamblePageModel
{
	public void OnGet()
	{
	}

	public async Task OnPostLoginAsync()
	{
		await HttpContext.ChallengeAsync("oidc");
	}
}