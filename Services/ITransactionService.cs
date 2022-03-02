using System.Collections.Generic;
using Fetch.Models;

namespace Fetch.Services
{
    public interface ITransactionService
    {
        void Add(Transaction transaction);

        IDictionary<string, int> GetGroupedPayerPoints();

        IEnumerable<Transaction> GetAll();
    }
}