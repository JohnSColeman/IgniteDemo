using Apache.Ignite.Core;
using Apache.Ignite.Core.Cache;
using Apache.Ignite.Linq;
using IgniteDemo.model;
using System;
using System.Linq;

namespace IgniteDemo
{
    public class ImportTransactions
    {
        private static readonly DateTime Jan1St1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly int CacheSize = 500;
        private readonly IIgnite _ignite;
        private readonly ICache<int, SimpleTransaction> _cache;

        public ImportTransactions(IIgnite ignite)
        {
            _ignite = ignite;
            _cache = ignite.GetCache<int, SimpleTransaction>("SimpleTransactions");
        }

        /** Push some transactions on the cache. */
        public void Execute()
        {
            // create cache entries
            SimpleTransaction[] trans = new SimpleTransaction[CacheSize];
            for (int k = 0; k < CacheSize; k++)
            {
                trans[k] = SimpleTransaction.NextSimpleTransaction();
            }

            // load cache
            var start = (DateTime.UtcNow - Jan1St1970).TotalMilliseconds;
            using (var ds = _ignite.GetDataStreamer<int, SimpleTransaction>("SimpleTransactions"))
            {
                for (int k = 0; k < CacheSize; k++)
                {
                    ds.AddData(trans[k].Id, trans[k]);
                }
            }
            var end = (DateTime.UtcNow - Jan1St1970).TotalMilliseconds;

            // report - all items should be status 1
            var queryable = _cache.AsCacheQueryable();
            var transactionList = queryable.Where(e => e.Value.EventStatus == 1).ToList();
            transactionList.ForEach(t => Console.WriteLine("imported: " + t.Value));
            var sum = queryable.AsEnumerable().Sum(x => x.Value.FeeAmount);
            var runtime = end - start;
            Console.WriteLine("total fees: " + sum);
            Console.WriteLine("import took " + runtime + "ms for " + _cache.GetSize() + " entries " + (runtime/_cache.GetSize()));
        }
    }
}