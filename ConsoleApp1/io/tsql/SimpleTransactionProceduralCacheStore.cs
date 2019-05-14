using Apache.Ignite.Core.Cache.Store;
using IgniteDemo.model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace IgniteDemo.io.tsql
{
    internal class SimpleTransactionProceduralCacheStore : CacheStoreAdapter<int, SimpleTransaction>
    {
        private readonly SqlConnection _conn = new SqlConnection("server=localhost,14333;database=igfindb;user=sa;password=Igdbpw1.");

        public SimpleTransactionProceduralCacheStore()
        {
            _conn.Open();
        }

        public override void Delete(int key)
        {
            using (SqlCommand cmd = new SqlCommand("dbo.delete_simple_transaction", _conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parm = new SqlParameter("@id", SqlDbType.Int);
                parm.Value = key;
                cmd.Parameters.Add(parm);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        public override SimpleTransaction Load(int key)
        {
            SimpleTransaction t = null;
            using (SqlCommand cmd = new SqlCommand("dbo.select_simple_transaction", _conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parm = new SqlParameter("@id", SqlDbType.Int);
                parm.Value = key;
                cmd.Parameters.Add(parm);
                var r = cmd.ExecuteReader();
                if (r.Read())
                {
                    t = new SimpleTransaction()
                    {
                        Id = (int)r["id"],
                        ChargeAmount = (decimal)r["charge_amount"],
                        FeeAmount = (decimal)r["fee_amount"],
                        EventStatus = (byte)r["event_status"]
                    };
                }
                cmd.Dispose();
            }
            return t;
        }

        public override void Write(int key, SimpleTransaction val)
        {
            using (SqlCommand cmd = new SqlCommand("dbo.updateorinsert_simple_transaction", _conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parmId = new SqlParameter("@id", SqlDbType.Int);
                SqlParameter parmChargeAmount = new SqlParameter("@charge_amount", SqlDbType.Decimal);
                parmChargeAmount.Precision = 18;
                parmChargeAmount.Scale = 6;
                SqlParameter parmFeeAmount = new SqlParameter("@fee_amount", SqlDbType.Decimal);
                parmChargeAmount.Precision = 18;
                parmChargeAmount.Scale = 6;
                SqlParameter parmEventStatus = new SqlParameter("@event_status", SqlDbType.TinyInt);

                parmId.Value = key;
                cmd.Parameters.Add(parmId);
                parmChargeAmount.Value = val.ChargeAmount;
                cmd.Parameters.Add(parmChargeAmount);
                parmFeeAmount.Value = val.FeeAmount;
                cmd.Parameters.Add(parmFeeAmount);
                parmEventStatus.Value = val.EventStatus;
                cmd.Parameters.Add(parmEventStatus);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        public override void SessionEnd(bool commit)
        {
            Console.WriteLine("SessionEnd " + commit);
            _conn.Close();
        }
    }
}