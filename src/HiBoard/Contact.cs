namespace HiBoard;

[Serializable]
public class Contact : Entity<Contact, int>
{
    public override int Id { get; protected set; }

    public string Name { get; set; } = null!;

    public DateTime? Birthdate { get; set; }
}
