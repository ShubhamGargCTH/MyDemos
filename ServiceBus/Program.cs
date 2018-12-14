using ServiceBus.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBus
{
    class Program
    {
        static void Main(string[] args)
        {
            TestQueueMessageSender sender = new TestQueueMessageSender();
            while (true)
            {
               
                sender.SendMessage("newTestMessage").GetAwaiter().GetResult();
                Console.WriteLine("Message Sent");
                Thread.Sleep(10000);
            }
              
        }
    }
}
