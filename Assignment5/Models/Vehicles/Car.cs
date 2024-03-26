using Assignment5.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5.Models.Vehicles
{
    public class Car : Vehicle
    {
        public FuelType FuelType { get; set; }

        public Car(string registrationNumber, string color, int nrOfWheels, FuelType fuelType, VehicleType vehicleType = VehicleType.Car) : base(registrationNumber, color, nrOfWheels, vehicleType)
        {
            FuelType = fuelType;
        }

        public override string ToString() => base.ToString() + $", Fuel Type: {FuelType}";
    }
}
