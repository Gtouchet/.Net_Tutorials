using BaseApi.Handlers;
using BaseApi.Handlers.CommandHandlers.AccountCommandHandlers;
using BaseApi.Handlers.CommandHandlers.AccountQueryHandlers;
using BaseApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Controllers;

[Route("[controller]")]
public class AccountController : Controller
{
	private readonly HandlersProcessor _handlers;

    public AccountController(HandlersProcessor handlersProcessor)
	{
        _handlers = handlersProcessor;
    }

    [HttpPost]
    public async Task<ActionResult<Account>> Create(CreateAccountCommand command)
    {
        Account account = (Account)await _handlers.ExecuteAsync(command);
        return Ok(account);
    }

    [HttpGet]
    public async Task<ActionResult<List<Account>>> Get(Guid? id = null)
    {
        List<Account> accounts = (List<Account>)await _handlers.ExecuteAsync(new GetAccountQuery(id));
        return Ok(accounts);
    }

    [HttpPut]
    public async Task<ActionResult<Account>> Update(UpdateAccountCommand command)
    {
        Account account = (Account)await _handlers.ExecuteAsync(command);
        return Ok(account);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _handlers.ExecuteAsync(new DeleteAccountCommand(id));
        return Ok();
    }
}
