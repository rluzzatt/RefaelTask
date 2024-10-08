using RefaelTask.Messages;

namespace RefaelTask.Interfaces
{
    public interface IReplier<out TRequestMessage, in TReplyMessage>
        where TRequestMessage : RequestMessage
        where TReplyMessage : ResponseMessage
    {
        IDisposable SubscribeRequests(Func<TRequestMessage, TReplyMessage> handler);
    }
}
