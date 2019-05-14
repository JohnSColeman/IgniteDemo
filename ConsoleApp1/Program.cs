using Apache.Ignite.Core;
using Apache.Ignite.Core.Cache.Configuration;
using IgniteDemo.model;
using System;
using System.Threading;

namespace IgniteDemo
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Ignite demo...");
            using (var ignite = Ignition.Start())
            {
                var factory = new SimpleTransactionCacheStoreFactory();
                QueryEntity[] entities =
                {
                    new QueryEntity(typeof(int), typeof(SimpleTransaction))
                };
                var cfg = new CacheConfiguration
                {
                    CacheStoreFactory = factory,
                    CacheMode = CacheMode.Partitioned,
                    OnheapCacheEnabled = true,
                    ReadThrough = true,
                    WriteThrough = true,
                    WriteBehindEnabled = true,
                    QueryEntities = entities,
                    Name = "SimpleTransactions"
                };
                ignite.AddCacheConfiguration(cfg);
                ignite.GetOrCreateCache<int, SimpleTransaction>(cfg);

                var producer = new ImportTransactions(ignite);
                var consumer1 = new FeeComputer(ignite);

                Thread consumer1Thread = new Thread(new ThreadStart(consumer1.Execute));
                consumer1Thread.Start();

                Thread importerThread = new Thread(new ThreadStart(producer.Execute));
                importerThread.Start();

                importerThread.Join();
                consumer1Thread.Join();

                Console.ReadKey();
            }
        }
    }
}