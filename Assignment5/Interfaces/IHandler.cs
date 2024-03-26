using Assignment5.Enums;
using Assignment5.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5.Interfaces
{
    public interface IHandler
    {
        bool Park(Vehicle vehicle);
        bool UnPark(string registrationNumber);

        bool IsRegistrationTaken(string registrationNumber);
        Vehicle? FindVehicleByReg(string registrationNumber);
        Dictionary<string, int> ListAllVehicles();
        List<Vehicle> SearchVehiclesByCriteria(string color = null, int? numberOfWheels = null, VehicleType? vehicleType = null);
        bool IsFull();
    }
}
