namespace RefaelTask.Model
{
    public class RequestMessage : TraceableMessage
    {
        public RequestMessage(Guid id) : base(id)
        {
        }
    }

    public class RequestTextMessage : RequestMessage
    {
        public string Text { get; set; }
        public RequestTextMessage(Guid id, string content) : base(id)
        {
            Text = content;
        }
    }
}
