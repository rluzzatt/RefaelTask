using RefaelTask.Interfaces;
using RefaelTask.Model;
using RefaelTask.Services;
using System.Collections.Generic;

namespace TestsDeleteme
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
           // Subscriber for RequestMessage(for replier to listen to)
            var requestSubscriber = new MessageSubscriber<RequestMessage>();

            // Publisher for RequestMessage (requester will use this to send requests)
            var requestPublisher = new MessagePublisher<RequestMessage>(requestSubscriber);

            // Subscriber for ResponseMessage (requester will use this to receive responses)
            var responseSubscriber = new MessageSubscriber<ResponseMessage>();

            // Publisher for ResponseMessage (replier will use this to send responses)
            var responsePublisher = new MessagePublisher<ResponseMessage>(responseSubscriber);

            // Create the Requester using requestPublisher and responseSubscriber
            Requester requester = new Requester(requestPublisher, responseSubscriber);

            // Create the Replier using responsePublisher and requestSubscriber
            Replier replier = new Replier(responsePublisher, requestSubscriber);

            // Subscribing the replier
            replier.SubscribeRequests(responseMessage =>
            {
                // Create a response message with the request ID
                return new ResponseMessage(Guid.NewGuid(), responseMessage.Id, $"this is the response message (to request:{responseMessage.Id}");
            });

            // Sending a request
            var requestMessage = new RequestMessage(Guid.NewGuid(), "Requesting data for xyz...");
            var responseMessage = await requester.Request(requestMessage);

            Console.WriteLine($"Received Response: {responseMessage.ResponseDetails}");
        }
    }


    /*
           MessageSubscriber<TextMessage> subscriber = new MessageSubscriber<TextMessage>();

           // Subscribe to the MessageReceived event
           subscriber.MessageReceived.Subscribe(msg =>
           {
               Console.WriteLine("Message received: " + msg);
           });

           // Create a publisher with a reference to the subscriber
           var publisher = new MessagePublisher<TextMessage>(subscriber);

           // Simulate publishing a message
           await publisher.Publish(new TextMessage(Guid.NewGuid(),"Hello from Publisher!"));

           */
}
