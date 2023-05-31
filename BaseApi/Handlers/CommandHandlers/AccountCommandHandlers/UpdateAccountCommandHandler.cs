using BaseApi.Handlers.Kernel;
using BaseApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BaseApi.Handlers.CommandHandlers.AccountCommandHandlers;

public record UpdateAccountCommand(
    Guid id,
    string newName,
    string newMail
) : ICommand;

public class UpdateAccountCommandHandler : HandlerBase, IHandler<UpdateAccountCommand, Account>
{
    public UpdateAccountCommandHandler(Context context, HandlersProcessor handlersProcessor) : base(context, handlersProcessor) { }

    public async Task<Account> Handle(UpdateAccountCommand message)
    {
        Account? account = await _context.Accounts
            .Where(account => account.Id == message.id)
            .FirstOrDefaultAsync();

        if (account == null)
        {
            throw new HandlerException(HttpStatusCode.NotFound, "Account not found");
        }

        if (_context.Accounts.Any(account => account.Mail == message.newMail && account.Id != message.id))
        {
            throw new HandlerException(HttpStatusCode.BadRequest, "Account already exists");
        }

        account.Name = !string.IsNullOrWhiteSpace(message.newName) ? message.newName : account.Name;
        account.Mail = !string.IsNullOrWhiteSpace(message.newMail) ? message.newMail : account.Mail;
        account.UpdatedAt = DateTimeOffset.UtcNow;
        await _context.SaveChangesAsync();

        return account;
    }
}
