using OkVip.Gamble.Samples;
using Xunit;

namespace OkVip.Gamble.EntityFrameworkCore.Domains;

[Collection(GambleTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<GambleEntityFrameworkCoreTestModule>
{
}