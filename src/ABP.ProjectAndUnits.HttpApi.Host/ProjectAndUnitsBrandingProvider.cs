using Microsoft.Extensions.Localization;
using ABP.ProjectAndUnits.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ABP.ProjectAndUnits;

[Dependency(ReplaceServices = true)]
public class ProjectAndUnitsBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<ProjectAndUnitsResource> _localizer;

    public ProjectAndUnitsBrandingProvider(IStringLocalizer<ProjectAndUnitsResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
