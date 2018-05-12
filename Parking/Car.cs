using System;

namespace ParkingCL
{
    public class Car : ICloneable
    {
        private object syncRoot = new object();
        public string LicensePlate { get; private set; }
        private decimal balance;
        public decimal Balance
        {
            get
            {
                lock (syncRoot)
                {
                    return balance;
                }
            }
            set
            {
                lock (syncRoot)
                {
                    balance = value;
                }
            }
        }
        public CarType CarType { get; private set; }

        public Car(string licensePlate, CarType carType, decimal balance = 0)
        {
            this.LicensePlate = licensePlate;
            this.CarType = carType;
            this.Balance = balance;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
