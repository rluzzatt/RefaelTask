using RefaelTask.Interfaces;
using RefaelTask.Model;
using System.Reactive.Linq;

namespace RefaelTask.Services
{
    public class Requester : IRequester<RequestMessage, ResponseMessage>
    {
        private readonly IPublisher<RequestMessage> _publisher;
        private readonly ISubscriber<ResponseMessage> _subscriber;

        public Requester(IPublisher<RequestMessage> publisher, ISubscriber<ResponseMessage> subscriber)
        {
            _publisher = publisher;
            _subscriber = subscriber;
        }

        public async Task<ResponseMessage> Request(RequestMessage requestMessage, CancellationToken cancellationToken = default)
        {
            // Publish the request
            Console.WriteLine($"publishing request: {requestMessage.RequestDetails} (Id:{requestMessage.Id})");

            await _publisher.Publish(requestMessage, cancellationToken);

            // Wait for the matching response
            var responseMessage = await _subscriber.MessageReceived
                .FirstAsync(response => response.RequestId == requestMessage.Id);

            return  responseMessage;
        }
    }

}
