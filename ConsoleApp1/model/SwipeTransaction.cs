using Apache.Ignite.Core.Cache.Configuration;
using System;

namespace IgniteDemo.model
{
    [Serializable]
    public class SwipeTransaction
    {
        [QuerySqlField(IsIndexed = true)]
        public int OrderId;

        [QuerySqlField]
        public long OrderPaymentId;

        [QuerySqlField]
        public DateTime? ReportingDate;

        [QuerySqlField]
        public long BusinessId;

        [QuerySqlField]
        public byte Gateway;

        [QuerySqlField]
        public decimal UsdAgodaFeeAmount;

        [QuerySqlField]
        public decimal LocalAgodaFeeAmount;

        [QuerySqlField]
        public decimal AgodaFeeAmount;

        [QuerySqlField]
        public decimal UsdFeeAmount;

        [QuerySqlField]
        public decimal FeeAmount;

        [QuerySqlField]
        public String FeeCurrency;

        [QuerySqlField]
        public decimal LocalAmount;

        [QuerySqlField]
        public String LocalCurrency;

        [QuerySqlField]
        public decimal LocalExchangeRate;

        [QuerySqlField]
        public decimal ChargeAmount;

        [QuerySqlField]
        public String ChargeCurrency;

        [QuerySqlField]
        public decimal ChargeExchangeRate;

        [QuerySqlField]
        public decimal NetAmount;

        [QuerySqlField]
        public decimal UsdNetAmount;

        [QuerySqlField]
        public String GatewayTransactionId;

        [QuerySqlField]
        public byte GatewayTransactionStatus;

        [QuerySqlField]
        public byte BusinessType;

        [QuerySqlField]
        public byte TransactionType;

        [QuerySqlField(IsIndexed = true)]
        public byte EventStatus;

        [QuerySqlField]
        public String DeviceName;

        [QuerySqlField]
        public String Remarks;

        [QuerySqlField]
        public byte SwipeCommissionType;

        [QuerySqlField]
        public decimal CommissionFee;

        [QuerySqlField]
        public decimal UpliftPercentage;

        [QuerySqlField]
        public decimal UpliftAmount;

        [QuerySqlField]
        public decimal UsdAmountPostUplift;

        [QuerySqlField]
        public byte PaymentProduct;

        private static int _id = 0;
        private static readonly Random Rnd = new Random();

        public SwipeTransaction()
        {
        }

        public static SwipeTransaction NextSwipeTransaction()
        {
            SwipeTransaction t = new SwipeTransaction();
            t.OrderId = ++_id;
            t.LocalCurrency = "THB";
            t.ChargeCurrency = "THB";
            t.GatewayTransactionId = "GatewayTransactionId";
            t.DeviceName = "";
            t.Remarks = "remarks...";
            t.EventStatus = 0;
            t.ChargeAmount = (decimal)Rnd.NextDouble();
            return t;
        }

        public SwipeTransaction(int orderId, long orderPaymentId, DateTime reportingDate,
                                long businessId, byte gateway, decimal usdAgodaFeeAmount,
                                decimal localAgodaFeeAmount, decimal agodaFeeAmount, decimal usdFeeAmount,
                                decimal feeAmount, String feeCurrency, decimal localAmount, String localCurrency,
                                decimal localExchangeRate, decimal chargeAmount, String chargeCurrency, decimal chargeExchangeRate,
                                decimal netAmount, decimal usdNetAmount, String gatewayTransactionId, byte gatewayTransactionStatus,
                                byte businessType, byte transactionType, byte eventStatus, String deviceName,
                                String remarks, byte swipeCommissionType, decimal commissionFee, decimal upliftPercentage,
                                decimal upliftAmount, decimal usdAmountPostUplift, byte paymentProduct)
        {
            this.OrderId = orderId;
            this.OrderPaymentId = orderPaymentId;
            this.ReportingDate = reportingDate;
            this.BusinessId = businessId;
            this.Gateway = gateway;
            this.UsdAgodaFeeAmount = usdAgodaFeeAmount;
            this.LocalAgodaFeeAmount = localAgodaFeeAmount;
            this.AgodaFeeAmount = agodaFeeAmount;
            this.UsdFeeAmount = usdFeeAmount;
            this.FeeAmount = feeAmount;
            this.FeeCurrency = feeCurrency;
            this.LocalAmount = localAmount;
            this.LocalCurrency = localCurrency;
            this.LocalExchangeRate = localExchangeRate;
            this.ChargeAmount = chargeAmount;
            this.ChargeCurrency = chargeCurrency;
            this.ChargeExchangeRate = chargeExchangeRate;
            this.NetAmount = netAmount;
            this.UsdNetAmount = usdNetAmount;
            this.GatewayTransactionId = gatewayTransactionId;
            this.GatewayTransactionStatus = gatewayTransactionStatus;
            this.BusinessType = businessType;
            this.TransactionType = transactionType;
            this.EventStatus = eventStatus;
            this.DeviceName = deviceName;
            this.Remarks = remarks;
            this.SwipeCommissionType = swipeCommissionType;
            this.CommissionFee = commissionFee;
            this.UpliftPercentage = upliftPercentage;
            this.UpliftAmount = upliftAmount;
            this.UsdAmountPostUplift = usdAmountPostUplift;
            this.PaymentProduct = paymentProduct;
        }

        public override string ToString()
        {
            return "SwipeTransaction{" +
                    "orderId=" + OrderId +
                    ", orderPaymentId=" + OrderPaymentId +
                    ", chargeAmount=" + ChargeAmount +
                    ", agodaFeeAmount=" + AgodaFeeAmount +
                    ", eventStatus=" + EventStatus +
                    '}';
        }
    }
}