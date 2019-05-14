using Apache.Ignite.Core;
using Apache.Ignite.Core.Cache;
using Apache.Ignite.Core.Cache.Event;
using Apache.Ignite.Core.Cache.Query.Continuous;
using IgniteDemo.model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IgniteDemo
{
    /**
     * Computes transaction fees using Ignites event handling.
     */
    public class FeeComputer
    {
        private readonly ICache<int, SimpleTransaction> _cache;

        public static void ComputeFees(SimpleTransaction simpleTransaction)
        {
            simpleTransaction.FeeAmount = simpleTransaction.ChargeAmount * 0.015m;
        }

        public FeeComputer(IIgnite ignite)
        {
            this._cache = ignite.GetCache<int, SimpleTransaction>("SimpleTransactions");
        }

        /** Run continuous query to capture update events. */
        public void Execute()
        {
            Console.WriteLine("running consumer...");
            var listener = new CacheEventListener();
            listener.OnCacheEntryEvent += a_ComputeFeesHandler;
            var qry = new ContinuousQuery<int, SimpleTransaction>(listener,
                new EventStatusEventFilter(0));
            using (_cache.QueryContinuous(qry))
            {
                Task.Delay(10000).Wait(); // need to keep query running
            }
        }

        /** Calculate the fees and update transaction state to 1. */
        private void a_ComputeFeesHandler(object sender, SimpleTransaction simpleTransaction)
        {
            ComputeFees(simpleTransaction);
            simpleTransaction.EventStatus = 1;
            _cache.Put(simpleTransaction.Id, simpleTransaction);
        }

        private class CacheEventListener : ICacheEntryEventListener<int, SimpleTransaction>
        {
            public event EventHandler<SimpleTransaction> OnCacheEntryEvent;

            public void OnEvent(IEnumerable<ICacheEntryEvent<int, SimpleTransaction>> evts)
            {
                foreach (var cacheEntryEvent in evts)
                {
                    OnCacheEntryEvent(this, cacheEntryEvent.Value);
                }
            }
        }

        private class EventStatusEventFilter : ICacheEntryEventFilter<int, SimpleTransaction>
        {
            private readonly byte _eventStatus;

            public EventStatusEventFilter(byte eventStatus)
            {
                _eventStatus = eventStatus;
            }

            public bool Evaluate(ICacheEntryEvent<int, SimpleTransaction> evt)
            {
                return evt.Value.EventStatus == _eventStatus;
            }
        }
    }
}