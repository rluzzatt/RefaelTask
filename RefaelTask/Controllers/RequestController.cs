using Microsoft.AspNetCore.Mvc;
using RefaelTask.Dto;
using RefaelTask.Interfaces;
using RefaelTask.Messages;

namespace RefaelTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IReplier<RequestTextMessage, ResponseTextMessage> _replier;
        private readonly IRequester<RequestTextMessage, ResponseTextMessage> _requester;

        public RequestController(IRequester<RequestTextMessage, ResponseTextMessage> requester,
            IReplier<RequestTextMessage, ResponseTextMessage> replier)
        {
            _replier = replier;
            _requester = requester;

            // Setup the subscription for handling requests
            _replier.SubscribeRequests(requestMessage =>
            {
                // Create a response message with the request ID
                return new ResponseTextMessage(Guid.NewGuid(), requestMessage.Id, $"this is the response message (to request:{requestMessage.Id})");
            });
        }

        [HttpGet]
        public async Task<ActionResult<List<RequestReplyDto>>> RequestRequestor()
        {
            var requestMessages = new List<RequestTextMessage>();

            for (int i = 0; i < 5; i++)
            {
                requestMessages.Add(new RequestTextMessage(Guid.NewGuid(), $"Requesting data for xyz... {i}"));
            }

            var result = new List<RequestReplyDto>();

            var tasks = requestMessages.Select(async requestMessage =>
            {
                var responseMessage = await _requester.Request(requestMessage);
                result.Add(new RequestReplyDto
                {
                    RequestTextMessage = requestMessage,
                    ResponseTextMessage = responseMessage
                });
            });

            await Task.WhenAll(tasks);

            return Ok(result);
        }
    }
}
