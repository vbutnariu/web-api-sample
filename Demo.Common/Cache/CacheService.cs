using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Common.Cache
{
    // https://michaelscodingspot.com/cache-implementations-in-csharp-net/
    public class Cache<TItem> : ICache<TItem>
    {
        private static MemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        private static ConcurrentDictionary<object, SemaphoreSlim> locks = new ConcurrentDictionary<object, SemaphoreSlim>();

        public TItem GetOrCreate(object key, Func<TItem> createItem)
        {
            if (!cache.TryGetValue(key, out TItem cacheEntry))
            {
                SemaphoreSlim mylock = locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
                mylock.Wait();
                try
                {
                    if (!cache.TryGetValue(key, out cacheEntry))
                    {
                        cacheEntry = createItem();
                        cache.Set(key, cacheEntry, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(1)));
                    }
                }
                finally
                {
                    mylock.Release();
                }
            }
            return cacheEntry;
        }

        public bool HasKey(object key)
        {
            return cache.TryGetValue(key, out TItem cacheEntry);
        }

        public TItem Get(object key)
        {
            cache.TryGetValue(key, out TItem cacheEntry);
            return cacheEntry;
        }

        public TItem GetOrCreate(object key, Func<TItem> createItem, MemoryCacheEntryOptions options)
        {
            if (!cache.TryGetValue(key, out TItem cacheEntry))
            {
                SemaphoreSlim mylock = locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

                mylock.Wait();
                try
                {
                    if (!cache.TryGetValue(key, out cacheEntry))
                    {
                        cacheEntry = createItem();
                        cache.Set(key, cacheEntry, options);
                    }
                }
                finally
                {
                    mylock.Release();
                }
            }
            return cacheEntry;
        }


        public async Task<TItem> GetOrCreateAsync(object key, Func<Task<TItem>> createItem)
        {
            if (!cache.TryGetValue(key, out TItem cacheEntry))
            {
                SemaphoreSlim mylock = locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

                await mylock.WaitAsync();
                try
                {
                    if (!cache.TryGetValue(key, out cacheEntry))
                    {
                        cacheEntry = await createItem();
                        cache.Set(key, cacheEntry, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(1)));
                    }
                }
                finally
                {
                    mylock.Release();
                }
            }
            return cacheEntry;
        }

        public async Task<TItem> GetOrCreateAsync(object key, Func<Task<TItem>> createItem, MemoryCacheEntryOptions options)
        {
            if (!cache.TryGetValue(key, out TItem cacheEntry))
            {
                SemaphoreSlim mylock = locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

                await mylock.WaitAsync();
                try
                {
                    if (!cache.TryGetValue(key, out cacheEntry))
                    {
                        cacheEntry = await createItem();
                        cache.Set(key, cacheEntry, options);
                    }
                }
                finally
                {
                    mylock.Release();
                }
            }
            return cacheEntry;
        }

        public void Remove(object key)
        {
            if (cache.TryGetValue(key, out TItem cacheEntry))
            {
                SemaphoreSlim mylock = locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

                mylock.Wait();
                try
                {
                    if (cache.TryGetValue(key, out cacheEntry))
                    {
                        cache.Remove(key);
                    }
                }
                finally
                {
                    mylock.Release();
                }
            }
        }
    }
}
