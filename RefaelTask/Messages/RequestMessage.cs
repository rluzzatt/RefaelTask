namespace RefaelTask.Messages
{
    public abstract class RequestMessage : TraceableMessage
    {
        public RequestMessage(Guid id) : base(id)
        {
        }
    }


}
