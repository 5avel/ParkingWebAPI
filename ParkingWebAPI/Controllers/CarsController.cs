using Microsoft.AspNetCore.Mvc;
using ParkingCL;
using System;
using System.Collections.Generic;

namespace ParkingWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Cars")]
    public class CarsController : Controller
    {
        private IParking _parking;

        public CarsController(IParking parking)
        {
            _parking = parking;
        }

        // GET: api/Cars Список всіх машин
        [HttpGet]
        public IEnumerable<Car> Get()
        {
            return _parking.GetAllCars();
        }

        // GET: api/Cars/5  Деталі по одній машині
        [HttpGet("{id}", Name = "Get")]
        public Car Get(string id)
        {
            return _parking.GetCarByID(id);
        }

        // POST: api/Cars  Додати машину 
        [HttpPost]
        public IActionResult Post([FromBody]Car value)
        {
            if (String.IsNullOrWhiteSpace(value.Id)) return BadRequest("The machine id can not be empty.");
            if(_parking.AddCar(value))
            {
                return Ok();
            }
            return BadRequest($"The machine with the number { value.Id} already exists. Please try again.");
        }

        // DELETE: api/ApiWithActions/5  Видалити машину
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            int res = _parking.DelCar(id);
            if (res == 1)
            {
                return Ok();
            }
            else if(res == -3)
            {
                return BadRequest(String.Format("The machine with the number {0} has a negative balance. Please Replenish balance.", id));
            }
            return BadRequest(String.Format("The machine with the number {0} is not found. Please try again.", id));
        }
    }
}
