using System;
using System.Collections.Generic;

namespace Simplic.Cache
{
    public interface IDataCacheRepository
    {
        T Get<T>(string type, string keyName, string key);

        void Set<T>(string type, string keyName, string key, T obj);

        void Remove(string type, string keyName, string key);
    }
}