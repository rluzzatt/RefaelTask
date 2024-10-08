using RefaelTask.Model;

namespace RefaelTask.Interfaces
{
    public interface IRequester<in TRequestMessage, TReplyMessage>
        where TRequestMessage : RequestMessage
        where TReplyMessage : ResponseMessage
    {
        Task<TReplyMessage> Request(TRequestMessage message, CancellationToken cancellationToken = default);
    }
}
