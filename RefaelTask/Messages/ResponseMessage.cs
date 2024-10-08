namespace RefaelTask.Messages
{
    public abstract class ResponseMessage : TraceableMessage
    {

        // Link to the related request
        public Guid RequestId { get; set; }

        public ResponseMessage(Guid id, Guid requestId) : base(id)
        {
            RequestId = requestId;
        }
    }


}
