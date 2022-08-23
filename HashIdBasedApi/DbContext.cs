using HashIdBasedApi.Models;

namespace HashIdBasedApi;

/// <summary>
/// In memory data storage
/// </summary>
public class DbContext
{
    public List<Account> Accounts = new List<Account>();

    public DbContext()
    {
        this.PopulateInMemoryDb();
    }

    private void PopulateInMemoryDb()
    {
        this.Accounts = new List<Account>()
        {
            new Account()
            {
                Id = 15,
                Mail = "jb95@gmail.com",
            },
            new Account()
            {
                Id = 4,
                Mail = "yvettedu75@orange.fr",
            },
            new Account()
            {
                Id = 9,
                Mail = "60momo@yahoo.fr",
            },
        };
    }
}
