using Microsoft.EntityFrameworkCore;
using OkVip.Gamble.Accounts;
using OkVip.Gamble.Announcements;
using OkVip.Gamble.Banks;
using OkVip.Gamble.Categories;
using OkVip.Gamble.Chips;
using OkVip.Gamble.EntityFrameworkCore.Mappings;
using OkVip.Gamble.Games;
using OkVip.Gamble.Rounds;
using OkVip.Gamble.Tickets;
using OkVip.Gamble.Transactions;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.Applications;
using Volo.Abp.OpenIddict.Authorizations;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.OpenIddict.Scopes;
using Volo.Abp.OpenIddict.Tokens;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace OkVip.Gamble.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ReplaceDbContext(typeof(IPermissionManagementDbContext))]
[ReplaceDbContext(typeof(ISettingManagementDbContext))]
[ReplaceDbContext(typeof(IFeatureManagementDbContext))]
[ReplaceDbContext(typeof(IOpenIddictDbContext))]
[ReplaceDbContext(typeof(IBackgroundJobsDbContext))]
[ReplaceDbContext(typeof(IAuditLoggingDbContext))]
[ConnectionStringName("Default")]
public class GambleDbContext :
    AbpDbContext<GambleDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext,
    IPermissionManagementDbContext,
    ISettingManagementDbContext,
    IFeatureManagementDbContext,
    IOpenIddictDbContext,
    IBackgroundJobsDbContext,
    IAuditLoggingDbContext
{
    public DbSet<IdentityUser> Users { get; set; }

    public DbSet<IdentityRole> Roles { get; set; }

    public DbSet<IdentityClaimType> ClaimTypes { get; set; }

    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }

    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }

    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    public DbSet<Tenant> Tenants { get; set; }

    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    public DbSet<PermissionGroupDefinitionRecord> PermissionGroups { get; set; }

    public DbSet<PermissionDefinitionRecord> Permissions { get; set; }

    public DbSet<PermissionGrant> PermissionGrants { get; set; }

    public DbSet<Setting> Settings { get; set; }

    public DbSet<SettingDefinitionRecord> SettingDefinitionRecords { get; set; }

    public DbSet<FeatureGroupDefinitionRecord> FeatureGroups { get; set; }

    public DbSet<FeatureDefinitionRecord> Features { get; set; }

    public DbSet<FeatureValue> FeatureValues { get; set; }

    public DbSet<OpenIddictApplication> Applications { get; set; }

    public DbSet<OpenIddictAuthorization> Authorizations { get; set; }

    public DbSet<OpenIddictScope> Scopes { get; set; }

    public DbSet<OpenIddictToken> Tokens { get; set; }

    public DbSet<BackgroundJobRecord> BackgroundJobs { get; set; }

    public DbSet<AuditLog> AuditLogs { get; set; }

    public DbSet<Game> Games { get; set; }

    public DbSet<GameLock> GameLocks { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Round> Rounds { get; set; }

    public DbSet<RoundDetail> RoundDetails { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<Chip> Chips { get; set; }

    public DbSet<Announcement> Announcements { get; set; }

    public DbSet<RoundCalculation> RoundCalculations { get; set; }

    public DbSet<Bank> Banks { get; set; }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<Ticket> Tickets { get; set; }

    public DbSet<TicketLog> TicketLogs { get; set; }

    public GambleDbContext(DbContextOptions<GambleDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        AbpSettingManagementDbProperties.DbTablePrefix = GambleDbProperties.DbTablePrefix;
        AbpPermissionManagementDbProperties.DbTablePrefix = GambleDbProperties.DbTablePrefix;
        AbpBackgroundJobsDbProperties.DbTablePrefix = GambleDbProperties.DbTablePrefix;
        AbpAuditLoggingDbProperties.DbTablePrefix = GambleDbProperties.DbTablePrefix;
        AbpIdentityDbProperties.DbTablePrefix = GambleDbProperties.DbTablePrefix;
        AbpOpenIddictDbProperties.DbTablePrefix = GambleDbProperties.DbTablePrefix;
        AbpFeatureManagementDbProperties.DbTablePrefix = GambleDbProperties.DbTablePrefix;
        AbpTenantManagementDbProperties.DbTablePrefix = GambleDbProperties.DbTablePrefix;

        base.OnModelCreating(builder);

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();
        builder.ConfigureCategory();
        builder.ConfigureGame();
        builder.ConfigureTransaction();
        builder.ConfigureRound();
        builder.ConfigureChip();
        builder.ConfigureAnnouncement();
        builder.ConfigureAccount();
        builder.ConfigureBank();
        builder.ConfigureTicket();
    }
}