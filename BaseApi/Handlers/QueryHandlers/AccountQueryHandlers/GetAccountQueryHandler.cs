using BaseApi.Handlers.Kernel;
using BaseApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BaseApi.Handlers.CommandHandlers.AccountQueryHandlers;

public record GetAccountQuery(
    Guid? id
) : IQuery;

public class GetAccountQueryHandler : HandlerBase, IHandler<GetAccountQuery, List<Account>>
{
    public GetAccountQueryHandler(Context context, HandlersProcessor handlersProcessor) : base(context, handlersProcessor) { }

    public async Task<List<Account>> Handle(GetAccountQuery message)
    {
        IQueryable<Account> query = _context.Accounts;
        if (message.id != null)
        {
            query = query.Where(account => account.Id == message.id);
        }

        List<Account> accounts = await query.ToListAsync();
        
        if (message.id != null && accounts.Count == 0)
        {
            throw new HandlerException(HttpStatusCode.NotFound, "Account not found");
        }

        return accounts;
    }
}
