﻿using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace OkVip.Gamble.Web;

[Dependency(ReplaceServices = true)]
public class GambleBrandingProvider : DefaultBrandingProvider
{
	public override string AppName => "Gamble";
}