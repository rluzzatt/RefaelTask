using RefaelTask.Interfaces;
using RefaelTask.Messages;
using System.Reactive.Linq;

namespace RefaelTask.Services
{
    public enum RequesterMode
    {
        Synchronous, // FIFO
        Asynchronous // First response returned
    }

    public class Requester : IRequester<RequestTextMessage, ResponseTextMessage>
    {
        private readonly IPublisher<RequestTextMessage> _publisher;
        private readonly ISubscriber<ResponseTextMessage> _subscriber;
        private readonly RequesterMode _requestMode;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public Requester(IPublisher<RequestTextMessage> publisher, 
            ISubscriber<ResponseTextMessage> subscriber, 
            RequesterMode requestMode = RequesterMode.Asynchronous)
        {
            _publisher = publisher;
            _subscriber = subscriber;
            _requestMode = requestMode;
        }

        public async Task<ResponseTextMessage> Request(RequestTextMessage RequestTextMessage, CancellationToken cancellationToken = default)
        {
            if (_requestMode == RequesterMode.Synchronous)
            {
                await _semaphore.WaitAsync(cancellationToken);
                try
                {
                    return await PublishAndAwaitResponse(RequestTextMessage, cancellationToken);
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            else
            {
                // Asynchronous mode: process without waiting for semaphore
                return await PublishAndAwaitResponse(RequestTextMessage, cancellationToken);
            }
        }

        private async Task<ResponseTextMessage> PublishAndAwaitResponse(RequestTextMessage RequestTextMessage, CancellationToken cancellationToken)
        {
            // Publish the request
            Console.WriteLine($"publishing request: {RequestTextMessage.Text} (Id:{RequestTextMessage.Id})");
            await _publisher.Publish(RequestTextMessage, cancellationToken);

            // Wait for the matching response
            return await _subscriber.MessageReceived
                .FirstAsync(response => response.RequestId == RequestTextMessage.Id);
        }

    }

}
