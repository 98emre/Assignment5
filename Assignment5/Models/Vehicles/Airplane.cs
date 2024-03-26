using Assignment5.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5.Models.Vehicles
{

    public class Airplane : Vehicle
    {


        public int NumberOfEngines { get; set; }

        public Airplane(string registrationNumber, string color, int nrOfWheels, int nrOfEngines, VehicleType type = VehicleType.Airplane) : base(registrationNumber, color, nrOfWheels, type)
        {
            NumberOfEngines = nrOfEngines;
        }

        public override string ToString() => base.ToString() + $", Number of engines: {NumberOfEngines}";

    }
}
