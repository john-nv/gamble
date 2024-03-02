using Volo.Abp.Identity.Localization;
using Volo.Abp.Identity.Settings;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace OkVip.Gamble.Settings;

public class GambleSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        context.Add(
            new SettingDefinition(
                GambleSettings.LuckspaceshipDefaultRate,
                1.98.ToString(),
                L("DisplayName:GambleSettings.Luckspaceship.Rate.Defaut"),
                L("Description:GambleSettings.Luckspaceship.Rate.Defaut"),
                true),

            new SettingDefinition(
                GambleSettings.AmountInVndUnit,
                24500.ToString(),
                L("DisplayName:GambleSettings.AmountInVndUnit"),
                L("Description:GambleSettings.AmountInVndUnit"),
                true),

            new SettingDefinition(
                IdentitySettingNames.Password.RequiredUniqueChars,
                0.ToString(),
                L("DisplayName:Abp.Identity.Password.RequiredUniqueChars"),
                L("Description:Abp.Identity.Password.RequiredUniqueChars"),
                true),

            new SettingDefinition(
                IdentitySettingNames.Password.RequireNonAlphanumeric,
                false.ToString(),
                L("DisplayName:Abp.Identity.Password.RequireNonAlphanumeric"),
                L("Description:Abp.Identity.Password.RequireNonAlphanumeric"),
                true),

            new SettingDefinition(
                IdentitySettingNames.Password.RequireLowercase,
                false.ToString(),
                L("DisplayName:Abp.Identity.Password.RequireLowercase"),
                L("Description:Abp.Identity.Password.RequireLowercase"),
                true),

            new SettingDefinition(
                IdentitySettingNames.Password.RequireUppercase,
                false.ToString(),
                L("DisplayName:Abp.Identity.Password.RequireUppercase"),
                L("Description:Abp.Identity.Password.RequireUppercase"),
                true),

            new SettingDefinition(
                IdentitySettingNames.Password.RequireDigit,
                false.ToString(),
                L("DisplayName:Abp.Identity.Password.RequireDigit"),
                L("Description:Abp.Identity.Password.RequireDigit"),
                true)
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<IdentityResource>(name);
    }
}