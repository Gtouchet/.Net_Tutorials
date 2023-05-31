using BaseApi.Handlers.Kernel;
using System.Reflection;

namespace BaseApi.Handlers;

public class UnknownHandlerException : Exception
{
    public UnknownHandlerException(Type type) : base($"Error, no handler found for message type {type.Name}") { }
}

public class HandlersProcessor
{
    private readonly IServiceProvider _serviceProvider;

    public HandlersProcessor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<object> ExecuteAsync<Message>(Message message) where Message : IMessage
    {
        Type? requestedHandler = Assembly
            .GetAssembly(typeof(HandlerBase))!
            .GetTypes()
            .Where(type => type.IsSubclassOf(typeof(HandlerBase)))
            .FirstOrDefault(handlerType => handlerType
                .GetInterfaces()
                .Any(handlerInterface => handlerInterface
                    .GetGenericArguments()
                    .Any(genericArgument => genericArgument == typeof(Message))))!;
        
        Type? handlerType = _serviceProvider
            .GetService(requestedHandler)!
            .GetType();
        
        object? handler = _serviceProvider.GetService(handlerType);
        if (handler == null)
        {
            throw new UnknownHandlerException(typeof(Message));
        }
        
        try
        {
            Task handleTask = (Task)handlerType
                .GetMethod("Handle")!
                .Invoke(handler, new object[] { message })!;
            
            return await handleTask
                .ContinueWith(task => task
                    .GetType()
                    .GetProperty("Result")!
                    .GetValue(task)!)!;
        }
        catch (TargetInvocationException exception)
        {
            throw exception.InnerException!;
        }
    }
}
