using System;

namespace ParkingCL
{
    public class Car : ICloneable
    {
        public string Id { get; private set; }
        public decimal Balance { set; get; }
        public CarType CarType { get; private set; }

        public Car(string id, CarType carType, decimal balance = 0)
        {
            this.Id = id;
            this.CarType = carType;
            this.Balance = balance;
        }

        public object Clone() => this.MemberwiseClone();

    }
}
