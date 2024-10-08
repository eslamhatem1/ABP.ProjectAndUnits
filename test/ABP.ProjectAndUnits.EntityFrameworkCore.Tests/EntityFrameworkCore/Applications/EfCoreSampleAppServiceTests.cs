using ABP.ProjectAndUnits.Samples;
using Xunit;

namespace ABP.ProjectAndUnits.EntityFrameworkCore.Applications;

[Collection(ProjectAndUnitsTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<ProjectAndUnitsEntityFrameworkCoreTestModule>
{

}
