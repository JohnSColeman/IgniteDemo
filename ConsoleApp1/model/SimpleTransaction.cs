using Apache.Ignite.Core.Cache.Configuration;
using System;

namespace IgniteDemo.model
{
    [Serializable]
    public class SimpleTransaction
    {
        [QuerySqlField(IsIndexed = true)]
        public int Id;

        [QuerySqlField]
        public decimal ChargeAmount;

        [QuerySqlField]
        public decimal FeeAmount;

        [QuerySqlField(IsIndexed = true)]
        public byte EventStatus;

        private static int _id = 0;
        private static readonly Random Rnd = new Random();

        public SimpleTransaction()
        {
        }

        public static SimpleTransaction NextSimpleTransaction()
        {
            SimpleTransaction t = new SimpleTransaction();
            t.Id = ++_id;
            t.EventStatus = 0;
            t.ChargeAmount = (decimal)Rnd.NextDouble();
            return t;
        }

        public override string ToString()
        {
            return "SwipeTransaction{" +
                    "Id=" + Id +
                    ", chargeAmount=" + ChargeAmount +
                    ", feeAmount=" + FeeAmount +
                    ", eventStatus=" + EventStatus +
                    '}';
        }
    }
}