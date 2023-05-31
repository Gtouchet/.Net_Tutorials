using BaseApi.Handlers.Kernel;
using BaseApi.Models;
using System.Net;

namespace BaseApi.Handlers.CommandHandlers.AccountCommandHandlers;

public record CreateAccountCommand(
    string name,
    string mail
) : ICommand;

public class CreateAccountCommandHandler : HandlerBase, IHandler<CreateAccountCommand, Account>
{
    public CreateAccountCommandHandler(Context context, HandlersProcessor handlersProcessor) : base(context, handlersProcessor) { }

    public async Task<Account> Handle(CreateAccountCommand message)
    {
        if (_context.Accounts.Any(account => account.Mail == message.mail))
        {
            throw new HandlerException(HttpStatusCode.BadRequest, "Account already exists");
        }

        if (string.IsNullOrWhiteSpace(message.mail))
        {
            throw new HandlerException(HttpStatusCode.BadRequest, "Mail is required");
        }

        Account account = new Account()
        {
            Name = message.name ?? "Anonymous",
            Mail = message.mail,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
        };
        
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        return account;
    }
}
