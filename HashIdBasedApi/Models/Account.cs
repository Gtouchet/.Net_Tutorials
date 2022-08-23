namespace HashIdBasedApi.Models;

/// <summary>
/// Internal account entity, containing the ID that should not be exposed
/// </summary>
public class Account
{
    public int Id { get; set; }
    public string Mail { get; set; }
}

/// <summary>
/// Exposed account resource, the ID is hashed by the API using a secret salt
/// </summary>
public class AccountResource
{
    public string HashId { get; set; }
    public string Mail { get; set; }
}