using Volo.Abp.Modularity;

namespace ABP.ProjectAndUnits;

/* Inherit from this class for your domain layer tests. */
public abstract class ProjectAndUnitsDomainTestBase<TStartupModule> : ProjectAndUnitsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
