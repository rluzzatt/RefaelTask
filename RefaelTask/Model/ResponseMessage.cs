namespace RefaelTask.Model
{
    public class ResponseMessage : TraceableMessage
    {

        // Link to the related request
        public Guid RequestId { get; set; }

        public ResponseMessage(Guid id, Guid requestId) : base(id)
        {
            RequestId = requestId;  
        }
    }

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
