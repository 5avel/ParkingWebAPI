using Microsoft.AspNetCore.Mvc;
using ParkingCL;
using System.Collections.Generic;

namespace ParkingWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Transactions")]
    public class TransactionsController : Controller
    {
        private Parking _parking = Parking.Instance;

        // GET: api/Transactions/log  Вивести Transactions.log
        [HttpGet("log")]
        public IEnumerable<string> GetLog()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Transactions Вивести транзакції за останню хвилину
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Transactions/id  Вивести транзакції за останню хвилину по одній конкретній машині.
        [HttpGet("{id}")]
        public IEnumerable<string> Get(string id)
        {
            return new string[] { "value1", "value2" };
        }

        // PUT: api/Transactions/5  Поповнити баланс машини
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]decimal value)
        {

        }
    }
}
