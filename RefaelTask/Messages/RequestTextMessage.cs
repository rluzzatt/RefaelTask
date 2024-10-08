namespace RefaelTask.Messages
{
    public class RequestTextMessage : RequestMessage
    {
        public string Text { get; set; }
        public RequestTextMessage(Guid id, string content) : base(id)
        {
            Text = content;
        }
    }
}
