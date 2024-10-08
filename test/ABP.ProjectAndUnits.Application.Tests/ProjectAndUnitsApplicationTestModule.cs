using Volo.Abp.Modularity;

namespace ABP.ProjectAndUnits;

[DependsOn(
    typeof(ProjectAndUnitsApplicationModule),
    typeof(ProjectAndUnitsDomainTestModule)
)]
public class ProjectAndUnitsApplicationTestModule : AbpModule
{

}
