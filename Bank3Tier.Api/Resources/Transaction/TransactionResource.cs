using System;
using Bank3Tier.Api.Resources.User;

namespace Bank3Tier.Api.Resources.Transaction
{
    public class TransactionResource
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Amount { get; set; }
        public string Mode { get; set; }
        public UserResource User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
