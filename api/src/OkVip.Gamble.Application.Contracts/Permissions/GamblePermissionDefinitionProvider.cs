using OkVip.Gamble.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace OkVip.Gamble.Permissions;

public class GamblePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(GamblePermissions.GroupName);

        DefineGamePermissions(myGroup);
        DefineCategoryPermissions(myGroup);
        DefineChipPermissions(myGroup);
        DefineAnnouncementPermissions(myGroup);
        DefineTransactionPermissions(myGroup);
        DefineTicketPermissions(myGroup);
    }

    public void DefineGamePermissions(PermissionGroupDefinition group)
    {
        group.AddPermission(name: GamblePermissions.Game.Default, L("GamePermissions:Default"));
        group.AddPermission(name: GamblePermissions.Game.Create, L("GamePermissions:Create"));
        group.AddPermission(name: GamblePermissions.Game.Update, L("GamePermissions:Update"));
        group.AddPermission(name: GamblePermissions.Game.Delete, L("GamePermissions:Delete"));
    }

    public void DefineCategoryPermissions(PermissionGroupDefinition group)
    {
        group.AddPermission(name: GamblePermissions.Category.Default, L("CategoryPermissions:Default"));
        group.AddPermission(name: GamblePermissions.Category.Create, L("CategoryPermissions:Create"));
        group.AddPermission(name: GamblePermissions.Category.Update, L("CategoryPermissions:Update"));
        group.AddPermission(name: GamblePermissions.Category.Delete, L("CategoryPermissions:Delete"));
    }

    public void DefineChipPermissions(PermissionGroupDefinition group)
    {
        group.AddPermission(name: GamblePermissions.Chip.Default, L("ChipPermissions:Default"));
        group.AddPermission(name: GamblePermissions.Chip.Create, L("ChipPermissions:Create"));
        group.AddPermission(name: GamblePermissions.Chip.Update, L("ChipPermissions:Update"));
        group.AddPermission(name: GamblePermissions.Chip.Delete, L("ChipPermissions:Delete"));
    }

    public void DefineAnnouncementPermissions(PermissionGroupDefinition group)
    {
        group.AddPermission(name: GamblePermissions.Announcement.Default, L("AnnouncementPermissions:Default"));
        group.AddPermission(name: GamblePermissions.Announcement.Create, L("AnnouncementPermissions:Create"));
        group.AddPermission(name: GamblePermissions.Announcement.Update, L("AnnouncementPermissions:Update"));
        group.AddPermission(name: GamblePermissions.Announcement.Delete, L("AnnouncementPermissions:Delete"));
    }

    public void DefineTransactionPermissions(PermissionGroupDefinition group)
    {
        group.AddPermission(name: GamblePermissions.Transaction.Default, L("TransactionPermissions:Default"));
        group.AddPermission(name: GamblePermissions.Transaction.Create, L("TransactionPermissions:Create"));
        group.AddPermission(name: GamblePermissions.Transaction.Update, L("TransactionPermissions:Update"));
        group.AddPermission(name: GamblePermissions.Transaction.Delete, L("TransactionPermissions:Delete"));
    }

    public void DefineTicketPermissions(PermissionGroupDefinition group)
    {
        group.AddPermission(name: GamblePermissions.Ticket.ApproveOrReject, L("TicketPermissions:ApproveOrReject"));
        group.AddPermission(name: GamblePermissions.Ticket.Delete, L("TicketPermissions:Delete"));
        group.AddPermission(name: GamblePermissions.Ticket.Create, L("TicketPermissions:Create"));
        group.AddPermission(name: GamblePermissions.Ticket.Update, L("TicketPermissions:Update"));
        group.AddPermission(name: GamblePermissions.Ticket.Default, L("TicketPermissions:Default"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<GambleResource>(name);
    }
}