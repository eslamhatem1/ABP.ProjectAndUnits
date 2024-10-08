using ABP.ProjectAndUnits.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ABP.ProjectAndUnits.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ProjectAndUnitsController : AbpControllerBase
{
    protected ProjectAndUnitsController()
    {
        LocalizationResource = typeof(ProjectAndUnitsResource);
    }
}
