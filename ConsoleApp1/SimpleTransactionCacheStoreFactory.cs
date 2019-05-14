using Apache.Ignite.Core.Cache.Store;
using Apache.Ignite.Core.Common;
using IgniteDemo.io.tsql;
using System;

namespace IgniteDemo
{
    [Serializable]
    public class SimpleTransactionCacheStoreFactory : IFactory<ICacheStore>
    {
        public ICacheStore CreateInstance()
        {
            return new SimpleTransactionProceduralCacheStore();
        }
    }
}