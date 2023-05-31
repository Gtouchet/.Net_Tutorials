using BaseApi.Handlers.Kernel;
using BaseApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BaseApi.Handlers.CommandHandlers.AccountCommandHandlers;

public record DeleteAccountCommand(
    Guid id
) : ICommand;

public class DeleteAccountCommandHandler : HandlerBase, IHandler<DeleteAccountCommand, bool>
{
    public DeleteAccountCommandHandler(Context context, HandlersProcessor handlersProcessor) : base(context, handlersProcessor) { }

    public async Task<bool> Handle(DeleteAccountCommand message)
    {
        Account? account = await _context.Accounts
            .Where(account => account.Id == message.id)
            .FirstOrDefaultAsync();

        if (account == null)
        {
            throw new HandlerException(HttpStatusCode.NotFound, "Account not found");
        }

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
        
        return true;
    }
}
