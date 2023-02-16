using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Demo.Common.Cache
{
    public interface ICache<TItem>
    {
        TItem GetOrCreate(object key, Func<TItem> createItem);
        TItem GetOrCreate(object key, Func<TItem> createItem, MemoryCacheEntryOptions options);
        Task<TItem> GetOrCreateAsync(object key, Func<Task<TItem>> createItem);
        Task<TItem> GetOrCreateAsync(object key, Func<Task<TItem>> createItem, MemoryCacheEntryOptions options);
        void Remove(object key);
        bool HasKey(object key);
        TItem Get(object key);
    }
}
