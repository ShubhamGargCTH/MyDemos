using AzureTableStorageRepository.Entities;
using AzureTableStorageRepository.Repositories;
using ServiceBusMessageConsumer.Consumers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusMessageConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new BaseRepository<DemoEntity>();
            TestConsumer consumer = new TestConsumer(repository);
            while (true)
            {
                Thread.Sleep(2000);
            }
            
        }
    }
}
