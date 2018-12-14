using AzureTableStorageRepository.Entities;
using AzureTableStorageRepository.Repositories;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusMessageConsumer.Consumers
{
    public class TestConsumer :BaseConsumer
    {
        private IBaseRepository<DemoEntity> _repository;

        public TestConsumer(IBaseRepository<DemoEntity> repository) : base("Test")
        {
            _repository = repository;
        }

        protected async override Task ProcessMessage(Message message, CancellationToken token)
        {
            // Process the message
            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");
            var rowId = Guid.NewGuid().ToString();
            //Save message in Azure Table
            _repository.AddEntity(new DemoEntity
            {
                DemoInt = message.SystemProperties.SequenceNumber,
                PartitionKey = message.PartitionKey,
                RowKey = rowId,
                DemoField = Encoding.UTF8.GetString(message.Body)
            });
            var entity = _repository.Get(x => x.RowKey == rowId)
                .Single();
            Console.WriteLine(entity.PartitionKey);
            _repository.Delete(entity);
            Console.WriteLine(_repository.Get(x => true).Count());

        }
    }
}
