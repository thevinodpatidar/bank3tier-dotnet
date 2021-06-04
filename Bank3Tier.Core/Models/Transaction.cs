﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank3Tier.Core.Models
{
    [Table("transactions", Schema = "bank")]
    public class Transaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Amount { get; set; }
        public string Mode { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
