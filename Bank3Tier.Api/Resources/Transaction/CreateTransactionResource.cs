using System;
namespace Bank3Tier.Api.Resources.Transaction
{
    public class CreateTransactionResource
    {
        public int Amount { get; set; }
        public string Mode { get; set; }
    }
}
