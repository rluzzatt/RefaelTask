using RefaelTask.Interfaces;
using RefaelTask.Model;
using System.Reactive.Linq;

namespace RefaelTask.Services
{
    public enum RequestMode
    {
        Synchronous, // FIFO
        Asynchronous // First response returned
    }

    public class Requester : IRequester<RequestMessage, ResponseMessage>
    {
        private readonly IPublisher<RequestMessage> _publisher;
        private readonly ISubscriber<ResponseMessage> _subscriber;
        private readonly RequestMode _requestMode;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public Requester(IPublisher<RequestMessage> publisher, 
            ISubscriber<ResponseMessage> subscriber, 
            RequestMode requestMode = RequestMode.Asynchronous)
        {
            _publisher = publisher;
            _subscriber = subscriber;
            _requestMode = requestMode;
        }

        public async Task<ResponseMessage> Request(RequestMessage requestMessage, CancellationToken cancellationToken = default)
        {
            if (_requestMode == RequestMode.Synchronous)
            {
                await _semaphore.WaitAsync(cancellationToken);
                try
                {
                    return await PublishAndAwaitResponse(requestMessage, cancellationToken);
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            else
            {
                // Asynchronous mode: process without waiting for semaphore
                return await PublishAndAwaitResponse(requestMessage, cancellationToken);
            }
        }

        private async Task<ResponseMessage> PublishAndAwaitResponse(RequestMessage requestMessage, CancellationToken cancellationToken)
        {
            // Publish the request
            Console.WriteLine($"publishing request: {requestMessage.RequestDetails} (Id:{requestMessage.Id})");
            await _publisher.Publish(requestMessage, cancellationToken);

            // Wait for the matching response
            return await _subscriber.MessageReceived
                .FirstAsync(response => response.RequestId == requestMessage.Id);
        }

    }

}
