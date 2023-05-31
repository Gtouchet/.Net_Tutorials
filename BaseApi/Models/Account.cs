namespace BaseApi.Models;

public class Account
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    public string Mail { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
