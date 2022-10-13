namespace MyModel;

public class UserAccount
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    public override string ToString()
    {
        return $"UserId: {Id}\tName: {Name}";
    }
}
