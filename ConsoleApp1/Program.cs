using Apache.Ignite.Core;
using Apache.Ignite.Core.Cache;
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
                ICache<int, SimpleTransaction> cache = ignite.GetOrCreateCache<int, SimpleTransaction>("SimpleTransactions");

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