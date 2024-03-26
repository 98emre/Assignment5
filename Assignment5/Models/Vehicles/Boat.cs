using Assignment5.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5.Models.Vehicles
{
    public class Boat : Vehicle
    {
        public double Length { get; set; }

        public Boat(string registrationNumber, string color, int nrOfWheels, double length, VehicleType type = VehicleType.Boat) : base(registrationNumber, color, nrOfWheels, type)
        {
            Length = length;
        }

        public override string ToString() => base.ToString() + $", Length: {Length:F1}";

    }
}
