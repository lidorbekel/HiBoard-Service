using HiBoard.Enums;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace HiBoard.Service.Resources;

[Serializable]
public class UserResource : Identifiable
{
    [Attr(Capabilities = AttrCapabilities.All)]
    public string UserName { get; set; } = string.Empty;

    [Attr(Capabilities = AttrCapabilities.All)]
    public string Title { get; set; } = string.Empty;

    [Attr(Capabilities = AttrCapabilities.All)]
    public UserRoles Role { get; set; }

    [Attr(Capabilities = AttrCapabilities.All)]
    public UserDepartments Department { get; set; }
}
