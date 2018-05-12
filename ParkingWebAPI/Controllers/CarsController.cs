using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingCL;

namespace ParkingWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Cars")]
    public class CarsController : Controller
    {
        private Parking _parking = Parking.Instance;

        // GET: api/Cars Список всіх машин
        [HttpGet]
        public IEnumerable<Car> Get()
        {
            return Parking.Instance.GetAllCars();
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
            if(_parking.AddCar(value))
            {
                return Ok();
            }

            return BadRequest(String.Format("The machine with the number {0} already exists. Please try again.", value.LicensePlate));
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
