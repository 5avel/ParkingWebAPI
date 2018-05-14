using Microsoft.AspNetCore.Mvc;
using ParkingCL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<Car>> Get()
            => await _parking.GetAllCarsAsync();
        
        // GET: api/Cars/5  Деталі по одній машині
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(string id)
        {
            var car = await _parking.GetCarByIdAsync(id);
            if (car == null) return BadRequest($"The machine with the number {id} not found.");
            return Ok(car);
        }

        // POST: api/Cars  Додати машину 
        [HttpPost]
        public IActionResult Post([FromBody]Car value)
        {
            if (String.IsNullOrWhiteSpace(value.Id)) return BadRequest("The machine id can not be empty.");
            Error res = _parking.AddCar(value);
            if (res == Error.Success)
                return Ok();
            else if(res == Error.ParkingIsFull)
                return BadRequest($"Parking Is Full. Please try again later.");
            else
                return BadRequest($"The machine with the number {value.Id} already exists. Please try again.");
        }

        // DELETE: api/ApiWithActions/5  Видалити машину
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Error res = _parking.DelCar(id);
            if (res == Error.Success)
            {
                return Ok($"The machine whis number {id} was successfully deleted.");
            }
            else if (res == Error.NegativeBalance)
            {
                return BadRequest($"The car with the number {id} has a negative balance. Please Replenish balance.");
            }
            
            return BadRequest($"The car with the number {id} is not found. Please try again.");
            
        }
    }
}
