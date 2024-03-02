﻿using Microsoft.Extensions.Configuration;
using OkVip.Gamble.Localization;
using OkVip.Gamble.MultiTenancy;
using System;
using System.Threading.Tasks;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace OkVip.Gamble.Web.Menus;

public class GambleMenuContributor : IMenuContributor
{
	private readonly IConfiguration _configuration;

	public GambleMenuContributor(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public async Task ConfigureMenuAsync(MenuConfigurationContext context)
	{
		if (context.Menu.Name == StandardMenus.Main)
		{
			await ConfigureMainMenuAsync(context);
		}
		else if (context.Menu.Name == StandardMenus.User)
		{
			await ConfigureUserMenuAsync(context);
		}
	}

	private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
	{
		var administration = context.Menu.GetAdministration();
		var l = context.GetLocalizer<GambleResource>();

		context.Menu.Items.Insert(
			0,
			new ApplicationMenuItem(
				GambleMenus.Home,
				l["Menu:Home"],
				"~/",
				icon: "fas fa-home",
				order: 0
			)
		);

		if (MultiTenancyConsts.IsEnabled)
		{
			administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
		}
		else
		{
			administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
		}

		administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
		administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

		return Task.CompletedTask;
	}

	private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
	{
		var l = context.GetLocalizer<GambleResource>();
		var accountStringLocalizer = context.GetLocalizer<AccountResource>();
		var authServerUrl = _configuration["AuthServer:Authority"] ?? "";

		context.Menu.AddItem(new ApplicationMenuItem("Account.Manage", accountStringLocalizer["MyAccount"],
			$"{authServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}", icon: "fa fa-cog", order: 1000, null, "_blank").RequireAuthenticated());
		context.Menu.AddItem(new ApplicationMenuItem("Account.Logout", l["Logout"], url: "~/Account/Logout", icon: "fa fa-power-off", order: int.MaxValue - 1000).RequireAuthenticated());

		return Task.CompletedTask;
	}
}