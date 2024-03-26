using Assignment5.Enums;
using Assignment5.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5.Models.Vehicles
{
    public abstract class Vehicle : IVehicle
    {
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }
        public int NrOfWheels { get; set; }

        public VehicleType Type { get; set; }

        public Vehicle(string registrationNumber, string color, int nrOfWheels, VehicleType type)
        {
            RegistrationNumber = registrationNumber;
            Color = color;
            NrOfWheels = nrOfWheels;
            Type = type;
        }


        public override string ToString() => $"{Type}, {RegistrationNumber}, {Color},Number of wheels: {NrOfWheels}";

    }
}
