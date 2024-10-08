﻿using RefaelTask.Interfaces;
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
            replier.SubscribeRequests(requestMessage =>
            {
                // Create a response message with the request ID
                return new ResponseMessage(Guid.NewGuid(), requestMessage.Id, $"this is the response message (to request:{requestMessage.Id}");
            });
            // Create a list of requests
            var requestMessages = new List<RequestMessage>();

            // Sending multiple requests and storing messages
            for (int i = 0; i < 5; i++)
            {
                requestMessages.Add(new RequestMessage(Guid.NewGuid(), $"Requesting data for xyz... {i}"));
            }

            // Create tasks for each request
            var tasks = requestMessages.Select(async requestMessage =>
            {
                var responseMessage = await requester.Request(requestMessage);
                Console.WriteLine($"Received Response: {responseMessage.ResponseDetails}");
            });

            // Await all tasks to ensure they complete
            await Task.WhenAll(tasks);
        }
    }

}
