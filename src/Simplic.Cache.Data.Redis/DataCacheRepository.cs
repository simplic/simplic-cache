using System.Threading.Tasks;

namespace Simplic.Cache.Data.Redis
{
    public class DataCacheRepository : IDataCacheRepository
    {
        public async Task<T> Get<T>(string type, string keyName, string key)
        {
            throw new System.NotImplementedException();
        }

        public async Task Remove(string type, string keyName, string key)
        {

        }

        public async Task Set<T>(string type, string keyName, string key, T obj)
        {

        }
    }
}
