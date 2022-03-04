using System.Collections.Generic;
using System.Linq;
using Fetch.Models;
using Fetch.Repositories;

namespace Fetch.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepo _transactionRepo;

        public TransactionService(ITransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }


        public void Add(Transaction transaction)
        {
            _transactionRepo.Add(transaction);
        }

        public IDictionary<string, int> GetGroupedPayerPoints()
        {
            return _transactionRepo.GetAll()
                                   .GroupBy(transaction => transaction.Payer)
                                   .ToDictionary(grouping => grouping.Key, grouping => grouping.Sum(transaction => transaction.PointsRunningTotal));
        }

        public IEnumerable<PayerPoints> Spend(int points) => _transactionRepo.Spend(points);
    }
}