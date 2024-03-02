using OkVip.Gamble.Samples;
using Xunit;

namespace OkVip.Gamble.EntityFrameworkCore.Applications;

[Collection(GambleTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<GambleEntityFrameworkCoreTestModule>
{
}