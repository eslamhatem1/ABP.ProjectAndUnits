using ABP.ProjectAndUnits.Samples;
using Xunit;

namespace ABP.ProjectAndUnits.EntityFrameworkCore.Domains;

[Collection(ProjectAndUnitsTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<ProjectAndUnitsEntityFrameworkCoreTestModule>
{

}
