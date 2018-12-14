using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Queues
{
    public class TestQueueMessageSender : BaseQueueMessageSender
    {
        public TestQueueMessageSender() :base("Test")
        {

        }
    }
}
