using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OkVip.Gamble.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gamble.Applications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ClientId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ClientSecret = table.Column<string>(type: "text", nullable: true),
                    ClientType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ConsentType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    DisplayNames = table.Column<string>(type: "text", nullable: true),
                    JsonWebKeySet = table.Column<string>(type: "text", nullable: true),
                    Permissions = table.Column<string>(type: "text", nullable: true),
                    PostLogoutRedirectUris = table.Column<string>(type: "text", nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true),
                    RedirectUris = table.Column<string>(type: "text", nullable: true),
                    Requirements = table.Column<string>(type: "text", nullable: true),
                    Settings = table.Column<string>(type: "text", nullable: true),
                    ClientUri = table.Column<string>(type: "text", nullable: true),
                    LogoUri = table.Column<string>(type: "text", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.Applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationName = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ImpersonatorUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ImpersonatorUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ImpersonatorTenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ImpersonatorTenantName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ExecutionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExecutionDuration = table.Column<int>(type: "integer", nullable: false),
                    ClientIpAddress = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ClientName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    ClientId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    CorrelationId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    BrowserInfo = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    HttpMethod = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    Url = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Exceptions = table.Column<string>(type: "text", nullable: true),
                    Comments = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    HttpStatusCode = table.Column<int>(type: "integer", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.BackgroundJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    JobArgs = table.Column<string>(type: "character varying(1048576)", maxLength: 1048576, nullable: false),
                    TryCount = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NextTryTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastTryTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsAbandoned = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Priority = table.Column<byte>(type: "smallint", nullable: false, defaultValue: (byte)15),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.BackgroundJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.ClaimTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: false),
                    IsStatic = table.Column<bool>(type: "boolean", nullable: false),
                    Regex = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    RegexDescription = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ValueType = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.ClaimTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.FeatureGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.FeatureGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.Features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ParentName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    DisplayName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    DefaultValue = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    IsVisibleToClients = table.Column<bool>(type: "boolean", nullable: false),
                    IsAvailableToHost = table.Column<bool>(type: "boolean", nullable: false),
                    AllowedProviders = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ValueType = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.FeatureValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ProviderKey = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.FeatureValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.LinkUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceTenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetTenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.LinkUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.OrganizationUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Code = table.Column<string>(type: "character varying(95)", maxLength: 95, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    EntityVersion = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.OrganizationUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gamble.OrganizationUnits_Gamble.OrganizationUnits_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Gamble.OrganizationUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Gamble.PermissionGrants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.PermissionGrants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.PermissionGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.PermissionGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ParentName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    DisplayName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    MultiTenancySide = table.Column<byte>(type: "smallint", nullable: false),
                    Providers = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    StateCheckers = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    IsStatic = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    EntityVersion = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.Scopes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Descriptions = table.Column<string>(type: "text", nullable: true),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    DisplayNames = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true),
                    Resources = table.Column<string>(type: "text", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.Scopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.SecurityLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ApplicationName = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: true),
                    Identity = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: true),
                    Action = table.Column<string>(type: "character varying(96)", maxLength: 96, nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    TenantName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ClientId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    CorrelationId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ClientIpAddress = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    BrowserInfo = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.SecurityLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.SettingDefinitions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    DefaultValue = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    IsVisibleToClients = table.Column<bool>(type: "boolean", nullable: false),
                    Providers = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    IsInherited = table.Column<bool>(type: "boolean", nullable: false),
                    IsEncrypted = table.Column<bool>(type: "boolean", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.SettingDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    ProviderName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ProviderKey = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    EntityVersion = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.UserDelegations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    SourceUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.UserDelegations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Surname = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    PasswordHash = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    SecurityStamp = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    IsExternal = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    PhoneNumber = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    ShouldChangePasswordOnNextLogin = table.Column<bool>(type: "boolean", nullable: false),
                    EntityVersion = table.Column<int>(type: "integer", nullable: false),
                    LastPasswordChangeTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.Authorizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true),
                    Scopes = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.Authorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gamble.Authorizations_Gamble.Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Gamble.Applications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Gamble.AuditLogActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuditLogId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    MethodName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Parameters = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    ExecutionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExecutionDuration = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.AuditLogActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gamble.AuditLogActions_Gamble.AuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalTable: "Gamble.AuditLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.EntityChanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuditLogId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ChangeType = table.Column<byte>(type: "smallint", nullable: false),
                    EntityTenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    EntityId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    EntityTypeFullName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.EntityChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gamble.EntityChanges_Gamble.AuditLogs_AuditLogId",
                        column: x => x.AuditLogId,
                        principalTable: "Gamble.AuditLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.OrganizationUnitRoles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.OrganizationUnitRoles", x => new { x.OrganizationUnitId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Gamble.OrganizationUnitRoles_Gamble.OrganizationUnits_Organ~",
                        column: x => x.OrganizationUnitId,
                        principalTable: "Gamble.OrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gamble.OrganizationUnitRoles_Gamble.Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Gamble.Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.RoleClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClaimType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gamble.RoleClaims_Gamble.Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Gamble.Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.TenantConnectionStrings",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Value = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.TenantConnectionStrings", x => new { x.TenantId, x.Name });
                    table.ForeignKey(
                        name: "FK_Gamble.TenantConnectionStrings_Gamble.Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Gamble.Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.UserClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClaimType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ClaimValue = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gamble.UserClaims_Gamble.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Gamble.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.UserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProviderKey = table.Column<string>(type: "character varying(196)", maxLength: 196, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.UserLogins", x => new { x.UserId, x.LoginProvider });
                    table.ForeignKey(
                        name: "FK_Gamble.UserLogins_Gamble.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Gamble.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.UserOrganizationUnits",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizationUnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.UserOrganizationUnits", x => new { x.OrganizationUnitId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Gamble.UserOrganizationUnits_Gamble.OrganizationUnits_Organ~",
                        column: x => x.OrganizationUnitId,
                        principalTable: "Gamble.OrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gamble.UserOrganizationUnits_Gamble.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Gamble.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Gamble.UserRoles_Gamble.Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Gamble.Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gamble.UserRoles_Gamble.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Gamble.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.UserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_Gamble.UserTokens_Gamble.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Gamble.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gamble.Tokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorizationId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Payload = table.Column<string>(type: "text", nullable: true),
                    Properties = table.Column<string>(type: "text", nullable: true),
                    RedemptionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReferenceId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Subject = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gamble.Tokens_Gamble.Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Gamble.Applications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Gamble.Tokens_Gamble.Authorizations_AuthorizationId",
                        column: x => x.AuthorizationId,
                        principalTable: "Gamble.Authorizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Gamble.EntityPropertyChanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    EntityChangeId = table.Column<Guid>(type: "uuid", nullable: false),
                    NewValue = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    OriginalValue = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    PropertyName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    PropertyTypeFullName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamble.EntityPropertyChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gamble.EntityPropertyChanges_Gamble.EntityChanges_EntityCha~",
                        column: x => x.EntityChangeId,
                        principalTable: "Gamble.EntityChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Applications_ClientId",
                table: "Gamble.Applications",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.AuditLogActions_AuditLogId",
                table: "Gamble.AuditLogActions",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.AuditLogActions_TenantId_ServiceName_MethodName_Exec~",
                table: "Gamble.AuditLogActions",
                columns: new[] { "TenantId", "ServiceName", "MethodName", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.AuditLogs_TenantId_ExecutionTime",
                table: "Gamble.AuditLogs",
                columns: new[] { "TenantId", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.AuditLogs_TenantId_UserId_ExecutionTime",
                table: "Gamble.AuditLogs",
                columns: new[] { "TenantId", "UserId", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Authorizations_ApplicationId_Status_Subject_Type",
                table: "Gamble.Authorizations",
                columns: new[] { "ApplicationId", "Status", "Subject", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.BackgroundJobs_IsAbandoned_NextTryTime",
                table: "Gamble.BackgroundJobs",
                columns: new[] { "IsAbandoned", "NextTryTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.EntityChanges_AuditLogId",
                table: "Gamble.EntityChanges",
                column: "AuditLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.EntityChanges_TenantId_EntityTypeFullName_EntityId",
                table: "Gamble.EntityChanges",
                columns: new[] { "TenantId", "EntityTypeFullName", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.EntityPropertyChanges_EntityChangeId",
                table: "Gamble.EntityPropertyChanges",
                column: "EntityChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.FeatureGroups_Name",
                table: "Gamble.FeatureGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Features_GroupName",
                table: "Gamble.Features",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Features_Name",
                table: "Gamble.Features",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.FeatureValues_Name_ProviderName_ProviderKey",
                table: "Gamble.FeatureValues",
                columns: new[] { "Name", "ProviderName", "ProviderKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.LinkUsers_SourceUserId_SourceTenantId_TargetUserId_T~",
                table: "Gamble.LinkUsers",
                columns: new[] { "SourceUserId", "SourceTenantId", "TargetUserId", "TargetTenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.OrganizationUnitRoles_RoleId_OrganizationUnitId",
                table: "Gamble.OrganizationUnitRoles",
                columns: new[] { "RoleId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.OrganizationUnits_Code",
                table: "Gamble.OrganizationUnits",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.OrganizationUnits_ParentId",
                table: "Gamble.OrganizationUnits",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.PermissionGrants_TenantId_Name_ProviderName_Provider~",
                table: "Gamble.PermissionGrants",
                columns: new[] { "TenantId", "Name", "ProviderName", "ProviderKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.PermissionGroups_Name",
                table: "Gamble.PermissionGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Permissions_GroupName",
                table: "Gamble.Permissions",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Permissions_Name",
                table: "Gamble.Permissions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.RoleClaims_RoleId",
                table: "Gamble.RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Roles_NormalizedName",
                table: "Gamble.Roles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Scopes_Name",
                table: "Gamble.Scopes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.SecurityLogs_TenantId_Action",
                table: "Gamble.SecurityLogs",
                columns: new[] { "TenantId", "Action" });

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.SecurityLogs_TenantId_ApplicationName",
                table: "Gamble.SecurityLogs",
                columns: new[] { "TenantId", "ApplicationName" });

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.SecurityLogs_TenantId_Identity",
                table: "Gamble.SecurityLogs",
                columns: new[] { "TenantId", "Identity" });

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.SecurityLogs_TenantId_UserId",
                table: "Gamble.SecurityLogs",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.SettingDefinitions_Name",
                table: "Gamble.SettingDefinitions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Settings_Name_ProviderName_ProviderKey",
                table: "Gamble.Settings",
                columns: new[] { "Name", "ProviderName", "ProviderKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Tenants_Name",
                table: "Gamble.Tenants",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Tokens_ApplicationId_Status_Subject_Type",
                table: "Gamble.Tokens",
                columns: new[] { "ApplicationId", "Status", "Subject", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Tokens_AuthorizationId",
                table: "Gamble.Tokens",
                column: "AuthorizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Tokens_ReferenceId",
                table: "Gamble.Tokens",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.UserClaims_UserId",
                table: "Gamble.UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.UserLogins_LoginProvider_ProviderKey",
                table: "Gamble.UserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.UserOrganizationUnits_UserId_OrganizationUnitId",
                table: "Gamble.UserOrganizationUnits",
                columns: new[] { "UserId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.UserRoles_RoleId_UserId",
                table: "Gamble.UserRoles",
                columns: new[] { "RoleId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Users_Email",
                table: "Gamble.Users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Users_NormalizedEmail",
                table: "Gamble.Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Users_NormalizedUserName",
                table: "Gamble.Users",
                column: "NormalizedUserName");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Users_UserName",
                table: "Gamble.Users",
                column: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gamble.AuditLogActions");

            migrationBuilder.DropTable(
                name: "Gamble.BackgroundJobs");

            migrationBuilder.DropTable(
                name: "Gamble.ClaimTypes");

            migrationBuilder.DropTable(
                name: "Gamble.EntityPropertyChanges");

            migrationBuilder.DropTable(
                name: "Gamble.FeatureGroups");

            migrationBuilder.DropTable(
                name: "Gamble.Features");

            migrationBuilder.DropTable(
                name: "Gamble.FeatureValues");

            migrationBuilder.DropTable(
                name: "Gamble.LinkUsers");

            migrationBuilder.DropTable(
                name: "Gamble.OrganizationUnitRoles");

            migrationBuilder.DropTable(
                name: "Gamble.PermissionGrants");

            migrationBuilder.DropTable(
                name: "Gamble.PermissionGroups");

            migrationBuilder.DropTable(
                name: "Gamble.Permissions");

            migrationBuilder.DropTable(
                name: "Gamble.RoleClaims");

            migrationBuilder.DropTable(
                name: "Gamble.Scopes");

            migrationBuilder.DropTable(
                name: "Gamble.SecurityLogs");

            migrationBuilder.DropTable(
                name: "Gamble.SettingDefinitions");

            migrationBuilder.DropTable(
                name: "Gamble.Settings");

            migrationBuilder.DropTable(
                name: "Gamble.TenantConnectionStrings");

            migrationBuilder.DropTable(
                name: "Gamble.Tokens");

            migrationBuilder.DropTable(
                name: "Gamble.UserClaims");

            migrationBuilder.DropTable(
                name: "Gamble.UserDelegations");

            migrationBuilder.DropTable(
                name: "Gamble.UserLogins");

            migrationBuilder.DropTable(
                name: "Gamble.UserOrganizationUnits");

            migrationBuilder.DropTable(
                name: "Gamble.UserRoles");

            migrationBuilder.DropTable(
                name: "Gamble.UserTokens");

            migrationBuilder.DropTable(
                name: "Gamble.EntityChanges");

            migrationBuilder.DropTable(
                name: "Gamble.Tenants");

            migrationBuilder.DropTable(
                name: "Gamble.Authorizations");

            migrationBuilder.DropTable(
                name: "Gamble.OrganizationUnits");

            migrationBuilder.DropTable(
                name: "Gamble.Roles");

            migrationBuilder.DropTable(
                name: "Gamble.Users");

            migrationBuilder.DropTable(
                name: "Gamble.AuditLogs");

            migrationBuilder.DropTable(
                name: "Gamble.Applications");
        }
    }
}
