using System.Collections.Generic;
using Fetch.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fetch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        [HttpPost]
        public void AddTransaction(Transaction transaction)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<PayerPoints> Spend(SpendRequest request)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Transaction> GetAll()
        {

        }
    }
}