using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusMessageConsumer.Consumers
{
    public abstract class BaseConsumer
    {
        //This will go to app setting
        const string ServiceBusConnectionString = "";

        IQueueClient queueClient;
        public BaseConsumer(string queueName)
        {
            queueClient = new QueueClient(ServiceBusConnectionString, queueName);
            // Register the function that will process messages
            queueClient.RegisterMessageHandler(ProcessMessagesAsync, GetMessageHandlerOptions());
        }

        protected virtual MessageHandlerOptions GetMessageHandlerOptions()
        {
            // Configure the MessageHandler Options in terms of exception handling, number of concurrent messages to deliver etc.
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                // Maximum number of Concurrent calls to the callback `ProcessMessagesAsync`, set to 1 for simplicity.
                // Set it according to how many messages the application wants to process in parallel.
                MaxConcurrentCalls = 1,

                // Indicates whether MessagePump should automatically complete the messages after returning from User Callback.
                // False below indicates the Complete will be handled by the User Callback as in `ProcessMessagesAsync` below.
                AutoComplete = false
            };
            return messageHandlerOptions;
            
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            await ProcessMessage(message, token);
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        protected abstract Task ProcessMessage(Message message, CancellationToken token);
           

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
