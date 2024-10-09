using RefaelTask.Messages;

namespace RefaelTask.Dto
{
    public class RequestReplyDto
    {
        public RequestTextMessage RequestTextMessage { get; set; }

        public ResponseTextMessage ResponseTextMessage { get; set; }
    }
}
