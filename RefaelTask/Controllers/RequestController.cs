using Microsoft.AspNetCore.Mvc;
using RefaelTask.Dto;
using RefaelTask.Interfaces;
using RefaelTask.Messages;
using RefaelTask.Services;

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
        }

        [HttpGet]
        public async Task<ActionResult<List<RequestReplyDto>>> RequestRequestor()
        {
            var requestMessageSubscriberForReplier = new MessageSubscriber<RequestTextMessage>();
            var requestMessagePublisherForRequester = new MessagePublisher<RequestTextMessage>(requestMessageSubscriberForReplier);

            var responseMessageSubscriberForRequester = new MessageSubscriber<ResponseTextMessage>();
            var responseMessagePublisherForReplier = new MessagePublisher<ResponseTextMessage>(responseMessageSubscriberForRequester);

            Requester requester = new Requester(requestMessagePublisherForRequester, responseMessageSubscriberForRequester);
            Replier replier = new Replier(responseMessagePublisherForReplier, requestMessageSubscriberForReplier);


            var result = new List<RequestReplyDto>();

            replier.SubscribeRequests(requestMessage =>
            {
                // Create a response message with the request ID
                return new ResponseTextMessage(Guid.NewGuid(), requestMessage.Id, $"this is the response message (to request:{requestMessage.Id}");
            });

            var requestMessages = new List<RequestTextMessage>();

            for (int i = 0; i < 5; i++)
            {
                requestMessages.Add(new RequestTextMessage(Guid.NewGuid(), $"Requesting data for xyz... {i}"));
            }

            // Create tasks for each request
            var tasks = requestMessages.Select(async requestMessage =>
            {
                var responseMessage = await requester.Request(requestMessage);
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
