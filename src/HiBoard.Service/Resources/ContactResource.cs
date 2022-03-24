using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;

namespace HiBoard.Service.Resources;

[Serializable]
public class ContactResource : Identifiable
{
    [Attr(PublicName = "name")]
    public string Name { get; set; } = null!;

    [Attr(PublicName = "birthdate")]
    public DateTime? Birthdate { get; set; }
}
