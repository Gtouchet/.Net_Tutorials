namespace BaseApi.Handlers.Kernel;

public interface IHandler<in TMessage, TResponse> where TMessage : IMessage
{
    Task<TResponse> Handle(TMessage message);
}
