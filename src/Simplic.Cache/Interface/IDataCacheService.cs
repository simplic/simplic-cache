using System;
using System.Collections.Generic;

namespace Simplic.Cache
{
    public interface IDataCacheService
    {
        T Get<T>(string type, string keyName, string key, Func<T> func);

        void Set<T>(string type, IDictionary<string, string> ids, T obj);

        void Remove(string type, IDictionary<string, string> ids);
    }
}