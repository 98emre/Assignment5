using Assignment5.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5.Models.Vehicles
{

    public class Motorcycle : Vehicle
    {
        public int CylinderVolume { get; set; }

        public Motorcycle(string registrationNumber, string color, int nrOfWheels, int cylinderVolume, VehicleType vehicleType = VehicleType.Motorcycle) : base(registrationNumber, color, nrOfWheels, vehicleType)
        {
            CylinderVolume = cylinderVolume;
        }

        public override string ToString() => base.ToString() + $", Cylinder volume: {CylinderVolume}";
    }
}
