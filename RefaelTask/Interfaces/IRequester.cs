using RefaelTask.Model;

namespace RefaelTask.Interfaces
{
    public interface IRequester<in TRequestMessage, TReplyMessage>
        where TRequestMessage : TraceableMessage
        where TReplyMessage : TraceableMessage
    {
        Task<TReplyMessage> Request(TRequestMessage message, CancellationToken cancellationToken = default);
    }
}
