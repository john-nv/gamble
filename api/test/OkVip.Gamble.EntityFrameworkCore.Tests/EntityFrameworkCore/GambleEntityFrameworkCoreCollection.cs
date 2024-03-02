using Xunit;

namespace OkVip.Gamble.EntityFrameworkCore;

[CollectionDefinition(GambleTestConsts.CollectionDefinitionName)]
public class GambleEntityFrameworkCoreCollection : ICollectionFixture<GambleEntityFrameworkCoreFixture>
{
}