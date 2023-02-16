using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Core.Caching
{
    public class MemoryCacheManager : ILocker, IDisposable
    {
        private readonly IMemoryCache memoryCache;
        private bool disposedValue;

        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        /// <summary>
        /// Perform some action with exclusive in-memory lock
        /// </summary>
        /// <param name="key">The key we are locking on</param>
        /// <param name="expirationTime">The time after which the lock will automatically be expired</param>
        /// <param name="action">Action to be performed with locking</param>
        /// <returns>True if lock was acquired and action was performed; otherwise false</returns>
        public bool PerformActionWithLock(string key, TimeSpan expirationTime, Action action)
        {
            //ensure that lock is acquired
            if (memoryCache.TryGetValue(key, out _))
                return false;

            try
            {
                memoryCache.Set(key, key, expirationTime);

                //perform action
                action();

                return true;
            }
            finally
            {
                //release lock even if action fails
                memoryCache.Remove(key);
            }
        }




        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    memoryCache.Dispose();
                }


                disposedValue = true;
            }
        }


        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
