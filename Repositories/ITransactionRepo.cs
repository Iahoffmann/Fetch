using System.Collections.Generic;
using Fetch.Models;

namespace Fetch.Repositories
{
    public interface ITransactionRepo
    {
        void Add(Transaction transaction);
        IEnumerable<TransactionRunningTotal> getAllRunningTotals();
        IEnumerable<Transaction> GetAll();
        IEnumerable<PayerPoints> Spend(int points);
    }
}