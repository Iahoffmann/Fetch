using System.Collections.Generic;
using System.Linq;
using Fetch.Models;

namespace Fetch.Repositories
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly Queue<Transaction> _transactions;

        public TransactionRepo()
        {
            _transactions = new Queue<Transaction>();
        }

        public void Add(Transaction transaction)
        {
            _transactions.Enqueue(transaction);
        }

        public IEnumerable<Transaction> GetAll() => _transactions.ToArray();

    }
}