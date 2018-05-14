using System;

namespace ParkingCL
{
    public class Transaction : ICloneable
    {
        public DateTime DateTime { get; private set; }
        public string Id { get; private set; }
        public decimal Debited { get; private set; }

        public Transaction(string id, decimal debited)
        {
            this.DateTime = DateTime.Now;
            this.Id = id;
            this.Debited = debited;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
