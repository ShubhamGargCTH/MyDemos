using AzureTableStorageRepository.CustomAttributes;
using AzureTableStorageRepository.Entities;
using Microsoft.Azure.CosmosDB.Table;
using Microsoft.Azure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AzureTableStorageRepository.Repositories
{
    /// <summary>
    /// Add table name using Table name custom attribute
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IBaseRepository<T>
        where T : BaseEntity, new()
    {
        CloudTable table;

        public BaseRepository()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("");
            CloudTableClient cloudTable = storageAccount.CreateCloudTableClient();
            string tableName = typeof(T).GetCustomAttributes(false)
                .OfType<TableNameAttribute>()
                .Select(x => x.TableName)
                .Single();

            table = cloudTable.GetTableReference(tableName);
            table.CreateIfNotExists();
        }

        public void AddEntity(T demoEntiy)
        {
            var operation = TableOperation.Insert(demoEntiy);
            table.Execute(operation);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            var query = table.CreateQuery<T>()
                .Where(predicate)
                .ToList();
            return query;
        }

        public void Delete(T demoEntiy)
        {
            var operation = TableOperation.Delete(demoEntiy);
            table.Execute(operation);
        }
    }
}
