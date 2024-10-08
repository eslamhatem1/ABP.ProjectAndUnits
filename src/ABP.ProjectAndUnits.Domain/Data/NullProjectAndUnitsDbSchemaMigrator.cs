using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ABP.ProjectAndUnits.Data;

/* This is used if database provider does't define
 * IProjectAndUnitsDbSchemaMigrator implementation.
 */
public class NullProjectAndUnitsDbSchemaMigrator : IProjectAndUnitsDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
