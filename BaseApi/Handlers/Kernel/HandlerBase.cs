namespace BaseApi.Handlers.Kernel;

public abstract class HandlerBase
{
    protected readonly Context _context;
    protected readonly HandlersProcessor _handlers;

    protected HandlerBase(Context context, HandlersProcessor handlersProcessor)
	{
        _context = context;
        _handlers = handlersProcessor;
    }
}
