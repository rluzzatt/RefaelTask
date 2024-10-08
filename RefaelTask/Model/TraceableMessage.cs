namespace RefaelTask.Model
{
    public abstract class TraceableMessage
    {
        protected TraceableMessage(Guid id)
        {
            Id = id;
            Timestamp = DateTime.Now;
        }
        public DateTime Timestamp { get; set; }

        public Guid Id { get; }

    }
}
