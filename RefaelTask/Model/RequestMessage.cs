namespace RefaelTask.Model
{
    public class RequestMessage : TraceableMessage
    {
        public string RequestDetails { get; set; }
        public RequestMessage(Guid id,string content) : base(id)
        {
            RequestDetails = content;
        }

        public override string ToString()
        {
            return $"{Timestamp}: Request {Id}";
        }
    }
}
