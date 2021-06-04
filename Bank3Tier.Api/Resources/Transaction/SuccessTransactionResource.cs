using System;
namespace Bank3Tier.Api.Resources.Transaction
{
    public class SuccessTransactionResource
    {
        public int Amount { get; set; }
        public int TotalBalance { get; set; }
        public string Status { get; set; }
    }
}
