using System;

namespace ParkingCL
{
    public class Transaction : ICloneable
    {
        public DateTime DateTime { get; private set; }
        public string CarLicensePlate { get; private set; }
        public decimal Debited { get; private set; }

        public Transaction(string carLicensePlate, decimal debited)
        {
            this.DateTime = DateTime.Now;
            this.CarLicensePlate = carLicensePlate;
            this.Debited = debited;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
