using Apache.Ignite.Core;
using Apache.Ignite.Core.Cache;
using Apache.Ignite.Core.Cache.Event;
using Apache.Ignite.Core.Cache.Query;
using Apache.Ignite.Core.Cache.Query.Continuous;
using IgniteDemo.model;
using System;
using System.Collections.Generic;

namespace IgniteDemo
{
    public class FeeComputer
    {
        private readonly Apache.Ignite.Core.Cache.ICache<int, SimpleTransaction> _cache;

        public FeeComputer(IIgnite ignite)
        {
            this._cache = ignite.GetCache<int, SimpleTransaction>("SimpleTransactions");
        }

        public void Execute()
        {
            Console.WriteLine("running consumer...");
            var eventListener = new EventListener();
            var qry = new ContinuousQuery<int, SimpleTransaction>(eventListener);
            var initialQry = new ScanQuery<int, SimpleTransaction>(new InitialFilter());
            using (var queryHandle = _cache.QueryContinuous(qry, initialQry))
            {
                foreach (var entry in queryHandle.GetInitialQueryCursor())
                {
                    var simpleTransaction = entry.Value;
                    ComputeFees(simpleTransaction);
                    simpleTransaction.EventStatus = 1;
                    _cache.Put(simpleTransaction.Id, simpleTransaction);
                    Console.WriteLine("computed: " + simpleTransaction);
                }
            }
        }

        private class EventListener : ICacheEntryEventListener<int, SimpleTransaction>
        {
            public void OnEvent(IEnumerable<ICacheEntryEvent<int, SimpleTransaction>> evts)
            {
                foreach (var evt in evts)
                    Console.WriteLine($"{evt.Value.Id}: {evt.Value.EventStatus}");
                // perform computation?
            }
        }

        public class InitialFilter : ICacheEntryFilter<int, SimpleTransaction>
        {
            public bool Invoke(ICacheEntry<int, SimpleTransaction> entry)
            {
                Console.WriteLine("Invoke");
                return entry.Value.EventStatus == 0;
            }
        }

        private void ComputeFees(SimpleTransaction simpleTransaction)
        {
            simpleTransaction.FeeAmount = simpleTransaction.ChargeAmount * 0.015m;
        }
    }
}