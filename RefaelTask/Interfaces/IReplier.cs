using RefaelTask.Model;

namespace RefaelTask.Interfaces
{
    public interface IReplier<out TRequestMessage, in TReplyMessage> 
        where TRequestMessage : TraceableMessage
        where TReplyMessage : TraceableMessage
    {
        IDisposable SubscribeRequests(Func<TRequestMessage, TReplyMessage> handler);
    }
}
