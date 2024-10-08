using Xunit;

namespace ABP.ProjectAndUnits.EntityFrameworkCore;

[CollectionDefinition(ProjectAndUnitsTestConsts.CollectionDefinitionName)]
public class ProjectAndUnitsEntityFrameworkCoreCollection : ICollectionFixture<ProjectAndUnitsEntityFrameworkCoreFixture>
{

}
