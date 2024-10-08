using System.Threading.Tasks;

namespace ABP.ProjectAndUnits.Data;

public interface IProjectAndUnitsDbSchemaMigrator
{
    Task MigrateAsync();
}
