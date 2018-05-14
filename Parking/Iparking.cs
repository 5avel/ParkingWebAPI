using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingCL
{
    public interface IParking
    {
        bool AddCar(Car car);
        int DelCar(string id);
        Car GetCarByID(string id);
        bool AddBalanceCar(string id, decimal money);
        decimal GetTotalParkingIncome();
        int CountFreeParkingPlaces();
        int CountOccupiedParkingPlaces();
        decimal GetIncomeLastMinute();
        List<Car> GetAllCars();
        List<Transaction> GetAllTransactions();
        List<Transaction> GetAllTransactionsById(string id);
        List<string> GetTransactionsLog();
    }
}
