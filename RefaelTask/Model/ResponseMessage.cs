namespace RefaelTask.Model
{
    public class ResponseMessage : TraceableMessage
    {
        public string ResponseDetails { get; set; }

        // Link to the related request
        public Guid RequestId { get; set; }

        public ResponseMessage(Guid id, Guid requestId, string responseDetails) : base(id)
        {
            RequestId = requestId;  
            ResponseDetails = responseDetails;
        }

        public override string ToString()
        {
            return $"{Timestamp}: Response to Request {RequestId} - Details: {ResponseDetails}";
        }
    }
}
