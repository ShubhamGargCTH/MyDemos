using AzureTableStorageRepository.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTableStorageRepository.Entities
{
    [TableName("Demo")]
    public class DemoEntity : BaseEntity
    {
        public string DemoField { get; set; }

        public long DemoInt { get; set; }
    }
}
