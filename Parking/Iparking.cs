using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParkingCL
{
    public interface IParking
    {
        Error AddCar(Car car);
        Error DelCar(string id);
        Task<Car> GetCarByIdAsync(string id);
        bool AddBalanceCar(string id, decimal money);
        decimal GetTotalParkingIncome();
        int CountFreeParkingPlaces();
        int CountOccupiedParkingPlaces();
        decimal GetIncomeLastMinute();
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<IEnumerable<Transaction>> GetAllTransactionsByIdAsync(string id);
        Task<List<string>> GetTransactionsLogAsync();
    }
}
