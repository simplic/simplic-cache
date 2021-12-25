using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Cache.Service
{
    public class DataCacheService : IDataCacheService
    {
        private readonly IDataCacheRepository dataCacheRepository;

        public DataCacheService(IDataCacheRepository dataCacheRepository)
        {
            this.dataCacheRepository = dataCacheRepository;
        }

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

        public async Task Remove(string type, IDictionary<string, string> keys)
        {
            if (keys == null)
                return;

            foreach (var kvp in keys)
                await dataCacheRepository.Remove(type, kvp.Key, kvp.Value);
        }

        public async Task Set<T>(string type, IDictionary<string, string> keys, T obj)
        {
            if (keys == null)
                return;

            foreach (var kvp in keys)
                await dataCacheRepository.Set(type, kvp.Key, kvp.Value, obj);
        }
    }
}
