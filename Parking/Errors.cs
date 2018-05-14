using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingCL
{
    public enum Error
    {
        Success,
        NotFound,
        AlreadyExists,
        ParkingIsFull,
        NegativeBalance,
        IsNullOrWhiteSpace,
        Error
    }
}
