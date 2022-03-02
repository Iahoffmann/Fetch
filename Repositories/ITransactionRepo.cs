using System.Collections.Generic;
using Fetch.Models;

namespace Fetch.Repositories
{
    public interface ITransactionRepo
    {
        void Add(Transaction transaction);
        IEnumerable<Transaction> GetAll();
        IEnumerable<PayerPoints> Spend(int points);
    }
}