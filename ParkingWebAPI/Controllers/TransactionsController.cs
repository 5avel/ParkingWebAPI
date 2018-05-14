using Microsoft.AspNetCore.Mvc;
using ParkingCL;
using System;
using System.Collections.Generic;

namespace ParkingWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Transactions")]
    public class TransactionsController : Controller
    {
        private IParking _parking;

        public TransactionsController(IParking parking)
        {
            _parking = parking;
        }

        // GET: api/Transactions/log  Вивести Transactions.log
        [HttpGet("log")]
        public IEnumerable<string> GetLog()
        {
            return _parking.GetTransactionsLog();
        }

        // GET: api/Transactions Вивести транзакції за останню хвилину
        [HttpGet]
        public IEnumerable<Transaction> Get()
        {
            return _parking.GetAllTransactions();
        }

        // GET: api/Transactions/id  Вивести транзакції за останню хвилину по одній конкретній машині.
        [HttpGet("{id}")]
        public IEnumerable<Transaction> Get(string id)
        {
            return _parking.GetAllTransactionsById(id);
        }

        // PUT: api/Transactions/5  Поповнити баланс машини
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]decimal value)
        {
            if(_parking.AddBalanceCar(id, value))
            {
                return Ok();
            }
            else
            {
                return BadRequest($"The machine with the number {id} is not found.");
            }

        }
    }
}
