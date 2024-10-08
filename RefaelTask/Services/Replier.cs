using RefaelTask.Interfaces;
using RefaelTask.Model;
using System;

namespace RefaelTask.Services
{
    public class Replier : IReplier<RequestMessage, ResponseMessage>
    {
        private readonly IPublisher<ResponseMessage> _publisher;
        private readonly ISubscriber<RequestMessage> _subscriber;
        private readonly Random _random = new();//for testing match request and response at different process order

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
                    // Introduce a random delay (between 0 to 2000 milliseconds)
                    var delay = _random.Next(0, 2000);
                    await Task.Delay(delay);

                    // Process the request message and generate a reply
                    var replyMessage = handler(requestMessage);

                    // Publish the reply
                    await _publisher.Publish(replyMessage);
                });
        }
    }

}
