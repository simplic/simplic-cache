using Newtonsoft.Json;
using Simplic.InMemoryDB;
using System.Threading.Tasks;

namespace Simplic.Cache.Data.Redis
{
    /// <inheritdoc/>
    public class DataCacheRepository : IDataCacheRepository
    {
        private readonly IKeyValueStore keyValueStore;

        /// <summary>
        /// Initialize repository
        /// </summary>
        /// <param name="keyValueStore">Key value store</param>
        public DataCacheRepository(IKeyValueStore keyValueStore)
        {
            this.keyValueStore = keyValueStore;
        }

        /// <inheritdoc/>
        public async Task<T> Get<T>(string type, string keyName, string key)
        {
            var value = await keyValueStore.StringGetAsync($"{type}_{keyName}_{key}");

            if (string.IsNullOrWhiteSpace(value))
                return default(T);

            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <inheritdoc/>
        public async Task Remove(string type, string keyName, string key)
        {
            await keyValueStore.RemoveSetAsync($"{type}_{keyName}_{key}");
        }

        /// <inheritdoc/>
        public async Task Set<T>(string type, string keyName, string key, T obj)
        {
            if (obj == null)
                return;

            var json = JsonConvert.SerializeObject(obj);

            await keyValueStore.StringSetAsync($"{type}_{keyName}_{key}", json);
        }
    }
}
