using System;
using System.Collections.Generic;
using System.Text;
using ABP.ProjectAndUnits.Localization;
using Volo.Abp.Application.Services;

namespace ABP.ProjectAndUnits;

/* Inherit your application services from this class.
 */
public abstract class ProjectAndUnitsAppService : ApplicationService
{
    protected ProjectAndUnitsAppService()
    {
        LocalizationResource = typeof(ProjectAndUnitsResource);
    }
}
