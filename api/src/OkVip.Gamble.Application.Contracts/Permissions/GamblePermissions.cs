namespace OkVip.Gamble.Permissions;

public static class GamblePermissions
{
    public const string GroupName = "Gamble";

    public static class Game
    {
        public const string Default = GroupName + ".Game.Default";

        public const string Create = GroupName + ".Game.Create";

        public const string Update = GroupName + ".Game.Update";

        public const string Delete = GroupName + ".Game.Delete";
    }

    public static class Category
    {
        public const string Default = GroupName + ".Category.Default";

        public const string Create = GroupName + ".Category.Create";

        public const string Update = GroupName + ".Category.Update";

        public const string Delete = GroupName + ".Category.Delete";
    }

    public static class Chip
    {
        public const string Default = GroupName + ".Chip.Default";

        public const string Create = GroupName + ".Chip.Create";

        public const string Update = GroupName + ".Chip.Update";

        public const string Delete = GroupName + ".Chip.Delete";
    }

    public static class Announcement
    {
        public const string Default = GroupName + ".Announcement.Default";

        public const string Create = GroupName + ".Announcement.Create";

        public const string Update = GroupName + ".Announcement.Update";

        public const string Delete = GroupName + ".Announcement.Delete";
    }

    public static class Transaction
    {
        public const string Default = GroupName + ".Transaction.Default";

        public const string Create = GroupName + ".Transaction.Create";

        public const string Update = GroupName + ".Transaction.Update";

        public const string Delete = GroupName + ".Transaction.Delete";
    }

    public static class Ticket
    {
        public const string ApproveOrReject = GroupName + ".Ticket.ApproveOrReject";

        public const string Delete = GroupName + ".Ticket.Delete";

        public const string Create = GroupName + ".Ticket.Create";

        public const string Update = GroupName + ".Ticket.Update";

        public const string Default = GroupName + ".Ticket.Default";
    }
}