using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBus
{
    public abstract class BaseQueueMessageSender : IDisposable
    {
        //This will go to App Setting
        const string ServiceBusConnectionString = "";
        IQueueClient queueClient;

        public BaseQueueMessageSender(string queueName)
        {
            queueClient = new QueueClient(ServiceBusConnectionString, queueName);
            
        }

        //This should access Message Type so that it can be used to construct other type of messages
        public virtual async Task SendMessage(string message)
        {
            await queueClient.SendAsync(new Message { Body = Encoding.UTF8.GetBytes(message)});
        }

        public void Dispose()
        {
            queueClient.CloseAsync();
        }   
    }
}
