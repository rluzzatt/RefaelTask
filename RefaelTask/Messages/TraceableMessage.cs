namespace RefaelTask.Messages
{
    public abstract class TraceableMessage
    {
        protected TraceableMessage(Guid id)
        {
            Id = id;
            Timestamp = DateTime.Now;
        }
        public DateTime Timestamp { get; }

        public Guid Id { get; }

    }
}
