using System;

namespace Fetch.Models
{
    public class Transaction
    {
        public string Payer { get; set; }

        public int Points { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}