using System.Collections.Generic;
using Fetch.Models;
using Fetch.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fetch.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>
        /// Used to add transactions
        /// </summary>
        /// <param name="transaction"></param>
        [HttpPost]
        public void AddTransaction(Transaction transaction)
        {
            _transactionService.Add(transaction);
        }

        /// <summary>
        /// Used to spend points
        /// </summary>
        /// <param name="request">A small transfer object to help clarify the naming</param>
        /// <returns>A list of Payers and the amount of points used in this spend action</returns>
        [HttpPost]
        public IEnumerable<PayerPoints> Spend(SpendRequest request)
        {
            return _transactionService.Spend(request.Points);
        }

        /// <summary>
        /// Used to get a summary of all payers and there current point totals
        /// </summary>
        /// <returns>A list of Payers and current point totals</returns>
        [HttpGet]
        public IDictionary<string, int> GetPayerPointTotals()
        {
            return _transactionService.GetGroupedPayerPoints();
        }
    }
}