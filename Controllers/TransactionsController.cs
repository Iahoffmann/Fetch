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
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        [HttpPost]
        public void AddTransaction(Transaction transaction)
        {
            _transactionService.Add(transaction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<PayerPoints> Spend(SpendRequest request)
        {
            return _transactionService.Spend(request.Points);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IDictionary<string, int> GetPayerPointTotals()
        {
            return _transactionService.GetGroupedPayerPoints();
        }
    }
}