using RefaelTask.Interfaces;
using RefaelTask.Messages;

namespace RefaelTask.Services
{
    public class MessagePublisher<TMessage> : IPublisher<TMessage> 
        where TMessage : TraceableMessage
    {
        private readonly MessageSubscriber<TMessage> _subscriber;

        public MessagePublisher(MessageSubscriber<TMessage> subscriber)
        {
            _subscriber = subscriber;
        }

        public async Task Publish(TMessage message, CancellationToken cancellationToken = default)
        {
            // Simulate some async work 
            await Task.Delay(100, cancellationToken);

            // Publish the message by pushing it to the subscriber
            _subscriber.OnMessageReceived(message);
        }
      
    }
}
