using RefaelTask.Interfaces;
using RefaelTask.Model;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RefaelTask.Services
{
    public class MessageSubscriber<TMessage> : ISubscriber<TMessage> 
        where TMessage : TraceableMessage
    {
        private readonly Subject<TMessage> _messageStream = new Subject<TMessage>();

        public IObservable<TMessage> MessageReceived => _messageStream.AsObservable();

        public void OnMessageReceived(TMessage message)
        {
            // Push the received message to the stream
            _messageStream.OnNext(message);
        }
    }
}
