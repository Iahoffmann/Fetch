using System;
using System.Collections.Generic;
using System.Linq;
using Fetch.Models;

namespace Fetch.Repositories
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly SortedList<DateTime, Transaction> _transactions;

        public TransactionRepo()
        {
            _transactions = new SortedList<DateTime, Transaction>();
        }

        public void Add(Transaction transaction)
        {
            _transactions.Add(transaction.TimeStamp, transaction);
        }

        public IEnumerable<Transaction> GetAll() => _transactions.Values;
        
        public IEnumerable<PayerPoints> Spend(int points)
        {
            var testTransactions = new List<Transaction>
            {
                new Transaction
                {
                    Payer = "DANNON",
                    Points = 1000,
                    TimeStamp = DateTime.Parse("2020-11-02T14:00:00Z")
                },
                new Transaction
                {
                    Payer = "UNILEVER",
                    Points = 200,
                    TimeStamp = DateTime.Parse("2020-10-31T11:00:00Z")
                },
                new Transaction
                {
                    Payer = "DANNON",
                    Points = -200,
                    TimeStamp = DateTime.Parse("2020-10-31T15:00:00Z")
                },
                new Transaction
                {
                    Payer = "MILLER COORS",
                    Points = 10000,
                    TimeStamp = DateTime.Parse("2020-11-01T14:00:00Z")
                },
                new Transaction
                {
                    Payer = "DANNON",
                    Points = 300,
                    TimeStamp = DateTime.Parse("2020-10-31T10:00:00Z")
                },
            };

            testTransactions.ForEach(x => _transactions.Add(x.TimeStamp, x));
            //TODO Remove these after testing

            var x = _transactions.Values;

            return new List<PayerPoints>();
        }
    }
}