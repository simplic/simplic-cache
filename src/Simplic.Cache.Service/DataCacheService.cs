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

        public T Get<T>(string type, string keyName, string key, Func<T> func)
        {
            var obj = dataCacheRepository.Get<T>(type, keyName, key);

            if (obj != null)
                return obj;

            obj = func();

            if (obj != null)
                dataCacheRepository.Set<T>(type, keyName, key, obj);

            return obj;
        }

        public void Remove(string type, IDictionary<string, string> keys)
        {
            if (keys == null)
                return;

            foreach (var kvp in keys)
                dataCacheRepository.Remove(type, kvp.Key, kvp.Value);
        }

        public void Set<T>(string type, IDictionary<string, string> keys, T obj)
        {
            if (keys == null)
                return;

            foreach (var kvp in keys)
                dataCacheRepository.Set(type, kvp.Key, kvp.Value, obj);
        }
    }
}
