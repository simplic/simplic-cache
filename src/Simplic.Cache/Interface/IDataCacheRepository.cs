using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplic.Cache
{
    public interface IDataCacheRepository
    {
        Task<T> Get<T>(string type, string keyName, string key);

        Task Set<T>(string type, string keyName, string key, T obj);
            
        Task Remove(string type, string keyName, string key);
    }
}