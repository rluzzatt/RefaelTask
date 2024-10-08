namespace RefaelTask.Model
{
    public class ResponseMessage : TraceableMessage
    {
        public string ResponseDetails { get; set; }

        // This property links the response to the original request
        public Guid RequestId { get; set; }

        public ResponseMessage(Guid id, Guid requestId, string responseDetails) : base(id)
        {
            RequestId = requestId;  // Link to the related request
            ResponseDetails = responseDetails;
        }

        public override string ToString()
        {
            return $"{Timestamp}: Response to Request {RequestId} - Details: {ResponseDetails}";
        }
    }
}
