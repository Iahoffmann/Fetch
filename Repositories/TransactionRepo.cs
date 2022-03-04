using System;
using System.Collections.Generic;
using System.Linq;
using Fetch.Models;

namespace Fetch.Repositories
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly SortedList<DateTime, TransactionRunningTotal> _transactions;

        public TransactionRepo()
        {
            _transactions = new SortedList<DateTime, TransactionRunningTotal>();
        }

        public void Add(Transaction transaction)
        {
            //There is no collision handling currently
            _transactions.Add(transaction.TimeStamp, new TransactionRunningTotal
            {
                Points = transaction.Points,
                TimeStamp = transaction.TimeStamp,
                Payer = transaction.Payer,
                PointsRunningTotal = transaction.Points
            });
        }

        public IEnumerable<TransactionRunningTotal> getAllRunningTotals() => _transactions.Values;

        public IEnumerable<Transaction> GetAll() => getAllRunningTotals();

        public IEnumerable<PayerPoints> Spend(int points)
        {
            TransactionRunningTotal partiallySpentTransaction = null;
            List<TransactionRunningTotal> spentTransactions = _transactions.SkipWhile(x => x.Value.PointsRunningTotal == 0)
                                                                           .TakeWhile(pair =>
                                                                           {
                                                                               (DateTime _, TransactionRunningTotal transaction) = pair;
                                                                               if (points - transaction.PointsRunningTotal >= 0)
                                                                               {
                                                                                   points -= transaction.PointsRunningTotal;
                                                                                   return true;
                                                                               }

                                                                               if (points - transaction.PointsRunningTotal < 0)
                                                                               {
                                                                                   partiallySpentTransaction = new TransactionRunningTotal
                                                                                   {
                                                                                       Payer = transaction.Payer,
                                                                                       Points = points, // Uses the remaining points as it's total
                                                                                       PointsRunningTotal = 0,
                                                                                       TimeStamp = transaction.TimeStamp
                                                                                   };
                                                                               }
                                                                               return false;
                                                                           })
                                                                           .Select(pair => pair.Value)
                                                                           .ToList();
            
            spentTransactions.Select(x => x.TimeStamp)
                             .ToList()
                             .ForEach(i => _transactions[i].PointsRunningTotal = 0);


            if (partiallySpentTransaction != null)
            {
                _transactions[partiallySpentTransaction.TimeStamp].PointsRunningTotal -= points;
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