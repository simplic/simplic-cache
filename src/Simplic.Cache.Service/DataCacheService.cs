using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplic.Cache.Service
{
    /// <inheritdoc/>
    public class DataCacheService : IDataCacheService
    {
        private readonly IDataCacheRepository dataCacheRepository;

        /// <summary>
        /// Initialize service
        /// </summary>
        /// <param name="dataCacheRepository">Repository instance</param>
        public DataCacheService(IDataCacheRepository dataCacheRepository)
        {
            this.dataCacheRepository = dataCacheRepository;
        }

        /// <inheritdoc/>
        public async Task<T> Get<T>(string type, string keyName, string key, Func<Task<T>> func)
        {
            var obj = await dataCacheRepository.Get<T>(type, keyName, key);

            if (obj != null)
                return obj;

            obj = await func();

            if (obj != null)
                await dataCacheRepository.Set<T>(type, keyName, key, obj);

            return obj;
        }

        /// <inheritdoc/>
        public async Task Remove(string type, IDictionary<string, string> keys)
        {
            if (keys == null)
                return;

            foreach (var kvp in keys)
                await dataCacheRepository.Remove(type, kvp.Key, kvp.Value);
        }

        /// <inheritdoc/>
        public async Task Set<T>(string type, IDictionary<string, string> keys, T obj)
        {
            if (keys == null)
                return;

            foreach (var kvp in keys)
                await dataCacheRepository.Set(type, kvp.Key, kvp.Value, obj);
        }
    }
}
