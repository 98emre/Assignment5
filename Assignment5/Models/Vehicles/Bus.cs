using Assignment5.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5.Models.Vehicles
{
    public class Bus : Vehicle
    {
        public int NumberOfSeats { get; set; }
        public Bus(string registrationNumber, string color, int nrOfWheels, int numberOfSeats, VehicleType vehicleType = VehicleType.Bus) : base(registrationNumber, color, nrOfWheels, vehicleType)
        {
            NumberOfSeats = numberOfSeats;
        }

        public override string ToString() => base.ToString() + $", Number of seats: {NumberOfSeats}";

    }
}
