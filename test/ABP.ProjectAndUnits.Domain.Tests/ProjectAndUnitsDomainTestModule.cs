using Volo.Abp.Modularity;

namespace ABP.ProjectAndUnits;

[DependsOn(
    typeof(ProjectAndUnitsDomainModule),
    typeof(ProjectAndUnitsTestBaseModule)
)]
public class ProjectAndUnitsDomainTestModule : AbpModule
{

}
