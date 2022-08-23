using HashIdBasedApi.Models;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;

namespace HashIdBasedApi.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly DbContext dbContext;
    private readonly IHashids hash;

    public AccountController(IHashids hash, DbContext dbContext)
    {
        this.hash = hash;
        this.dbContext = dbContext;
    }

    /// <summary>
    /// Expose a specific account by its hashed ID (returns the account with its raw ID for testing purpose) <br/>
    /// Or expose all accounts as resources if the ID parameter is null
    /// </summary>
    /// <param name="id">The hashed ID of the account</param>
    /// <returns>A specific account if found, or all accounts</returns>
    [HttpGet]
    public ActionResult Get(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            List<AccountResource> accountResources = new List<AccountResource>();

            this.dbContext.Accounts.ForEach(a => accountResources.Add(new AccountResource()
            {
                HashId = this.hash.Encode(a.Id),
                Mail = a.Mail,
            }));

            return Ok(accountResources);
        }

        int rawId;
        try
        {
            rawId = this.hash.DecodeSingle(id);
        }
        catch (NoResultException)
        {
            return NotFound();
        }

        Account account = this.dbContext.Accounts.Find(a => a.Id.Equals(rawId));
        if (account == null)
        {
            return NotFound();
        }

        AccountResource accountResource = new AccountResource()
        {
            HashId = account.Id.ToString(), // this.hash.Encode(account.Id), <- to hash and hide the raw ID
            Mail = account.Mail,
        };

        return Ok(accountResource);
    }
}