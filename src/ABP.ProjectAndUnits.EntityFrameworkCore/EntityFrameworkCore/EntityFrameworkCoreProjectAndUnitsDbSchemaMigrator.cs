using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ABP.ProjectAndUnits.Data;
using Volo.Abp.DependencyInjection;

namespace ABP.ProjectAndUnits.EntityFrameworkCore;

public class EntityFrameworkCoreProjectAndUnitsDbSchemaMigrator
    : IProjectAndUnitsDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreProjectAndUnitsDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the ProjectAndUnitsDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ProjectAndUnitsDbContext>()
            .Database
            .MigrateAsync();
    }
}
