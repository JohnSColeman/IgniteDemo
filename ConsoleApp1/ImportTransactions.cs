using Apache.Ignite.Core;
using Apache.Ignite.Core.Cache;
using IgniteDemo.model;
using System;

namespace IgniteDemo
{
    public class ImportTransactions
    {
        private readonly Apache.Ignite.Core.Cache.ICache<int, SimpleTransaction> _cache;

        public ImportTransactions(IIgnite ignite)
        {
            this._cache = ignite.GetCache<int, SimpleTransaction>("SimpleTransactions");
        }

        /** Push some transactions on the cache. */

        public void Execute()
        {
            for (int key = 0; key < 100; key++)
            {
                var tran = SimpleTransaction.NextSimpleTransaction();
                _cache.Put(tran.Id, tran);
            }

            foreach (ICacheEntry<int, SimpleTransaction> entry in _cache)
                Console.WriteLine("imported: " + entry);
        }
    }
}