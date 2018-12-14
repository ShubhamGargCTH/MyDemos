using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Azure.CosmosDB.Table;

namespace AzureTableStorageRepository.Repositories
{
    public interface IBaseRepository<T> where T : TableEntity, new()
    {
        void AddEntity(T demoEntiy);
        void Delete(T demoEntiy);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
    }
}