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
            //TODO add collision handling
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
            Transaction partiallySpentTransaction = null;

            List<Transaction> spentTransactions = _transactions.TakeWhile(pair =>
                {
                    (DateTime _key, Transaction transaction) = pair;
                    if (points - transaction.Points >= 0)
                    {
                        points -= transaction.Points;
                        return true;
                    }

                    if (points - transaction.Points < 0)
                    {
                        partiallySpentTransaction = new Transaction
                        {
                            Payer = transaction.Payer,
                            Points = transaction.Points,
                            TimeStamp = transaction.TimeStamp
                        };
                    }
                    return false;
                })
                .Select(pair => pair.Value)
                .ToList();
            
            spentTransactions.ForEach(transaction => _transactions.Remove(transaction.TimeStamp));

            if (partiallySpentTransaction != null)
            {
                // Uses the remaining points as it's total
                partiallySpentTransaction.Points = points;
                _transactions[partiallySpentTransaction.TimeStamp].Points -= points;
                spentTransactions.Add(partiallySpentTransaction);
            }

            return spentTransactions.GroupBy(transaction => transaction.Payer)
                                    .Select(
                                        grouping => new PayerPoints
                                        {
                                            Payer = grouping.Key,
                                            Points = grouping.Sum(transaction => transaction.Points) * -1
                                        }
                                    );
        }
    }
}