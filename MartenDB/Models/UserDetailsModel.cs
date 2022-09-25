namespace MartenDB.Models;

public record UserDetails()
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
};