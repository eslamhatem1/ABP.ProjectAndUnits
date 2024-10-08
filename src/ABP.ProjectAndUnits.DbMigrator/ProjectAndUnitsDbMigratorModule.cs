using ABP.ProjectAndUnits.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace ABP.ProjectAndUnits.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ProjectAndUnitsEntityFrameworkCoreModule),
    typeof(ProjectAndUnitsApplicationContractsModule)
    )]
public class ProjectAndUnitsDbMigratorModule : AbpModule
{
}
