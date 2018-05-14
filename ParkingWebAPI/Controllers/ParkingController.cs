using Microsoft.AspNetCore.Mvc;
using ParkingCL;

namespace ParkingWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Parking")]
    public class ParkingController : Controller
    {
        private IParking _parking;

        public ParkingController(IParking parking)
        {
            _parking = parking;
        }

        // GET: api/Parking/free   Кількість вільних місць
        [HttpGet("free")]
        public int GetFree()
        {
            return _parking.CountFreeParkingPlaces();
        }

        // GET: api/Parking/occupied   Кількість зайнятих місць
        [HttpGet("occupied")]
        public int GetOccupied()
        {
            return _parking.CountOccupiedParkingPlaces();
        }

        // GET: api/Parking/free   Загальний дохід
        [HttpGet("income")]
        public decimal GetTotalIncome()
        {
            return _parking.GetTotalParkingIncome();
        }
    }
}
