using RefaelTask.Interfaces;
using RefaelTask.Model;

namespace RefaelTask.Services
{
    public class Replier : IReplier<RequestMessage, ResponseMessage>
    {
        private readonly IPublisher<ResponseMessage> _publisher;
        private readonly ISubscriber<RequestMessage> _subscriber;


        public Replier(IPublisher<ResponseMessage> publisher, ISubscriber<RequestMessage> subscriber)
        {
            _publisher = publisher;
            _subscriber = subscriber;
        }

        public IDisposable SubscribeRequests(Func<RequestMessage, ResponseMessage> handler)
        {
            return _subscriber.MessageReceived
                .Subscribe(async requestMessage =>
                {
                    // Process the request message and generate a reply
                    var replyMessage = handler(requestMessage);

                    // Publish the reply
                    await _publisher.Publish(replyMessage);
                });
        }
    }

}
