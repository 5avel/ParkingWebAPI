using Microsoft.AspNetCore.Mvc;
using ParkingCL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<string>> GetLog() 
            => await _parking.GetTransactionsLogAsync();

        // GET: api/Transactions Вивести транзакції за останню хвилину
        [HttpGet]
        public async Task<IEnumerable<Transaction>> Get()
            => await _parking.GetAllTransactionsAsync();
        

        // GET: api/Transactions/id  Вивести транзакції за останню хвилину по одній конкретній машині.
        [HttpGet("{id}")]
        public async Task<IEnumerable<Transaction>> Get(string id)
            =>  await _parking.GetAllTransactionsByIdAsync(id);
        

        // PUT: api/Transactions/5  Поповнити баланс машини
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]decimal value)
        {
            if(value <= 0) return BadRequest("The recharge amount must be greater than zero.");
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
