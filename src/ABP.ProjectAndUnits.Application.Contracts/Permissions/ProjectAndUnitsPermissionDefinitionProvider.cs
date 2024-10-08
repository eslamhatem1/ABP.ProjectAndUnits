using ABP.ProjectAndUnits.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ABP.ProjectAndUnits.Permissions;

public class ProjectAndUnitsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ProjectAndUnitsPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ProjectAndUnitsPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ProjectAndUnitsResource>(name);
    }
}
