namespace RefaelTask.Messages
{
    public class ResponseTextMessage : ResponseMessage
    {
        public string Text { get; set; }

        public ResponseTextMessage(Guid id, Guid requestId, string responseDetails)
            : base(id, requestId)
        {
            Text = responseDetails;
        }
    }
}
