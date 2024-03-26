using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment5.Enums;

namespace Assignment5.Interfaces
{
    public interface IVehicle
    {
        string RegistrationNumber { get; set; }
        string Color { get; set; }
        int NrOfWheels { get; set; }

        VehicleType Type { get; set; }
    }
}
