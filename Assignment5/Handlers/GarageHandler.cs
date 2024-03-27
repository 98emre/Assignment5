using Assignment5.Enums;
using Assignment5.Interfaces;
using Assignment5.Models;
using Assignment5.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5.Handlers
{
    public class GarageHandler : IGarageHandler
    {

        private readonly Garage<IVehicle> garage;

        public GarageHandler(int capacity)
        {
            garage = new Garage<IVehicle>(capacity);
        }

        public Garage<IVehicle> Garage { get { return garage; } }

        public IVehicle? FindVehicleByReg(string registrationNumber) => Garage.FindVehicleByReg(registrationNumber);

        public int GarageCapacity() => Garage.Capacity;

        public int NumberOfVehicles() => Garage.Size;

        public bool IsFull() => Garage.Size == Garage.Capacity;

        public bool IsRegistrationTaken(string registrationNumber) => Garage.IsRegistrationTaken(registrationNumber);

        public Dictionary<string, int> ListAllVehicles() => Garage.ListAllVehicles();

        public bool Park(IVehicle vehicle) => Garage.Park(vehicle);

        public bool UnPark(string registrationNumber) => Garage.UnPark(registrationNumber);

        public List<IVehicle> SearchVehiclesByCriteria(string? color = null, int? numberOfWheels = null, VehicleType? vehicleType = null)
        {
            return Garage.SearchVehiclesByCriteria(color,numberOfWheels,vehicleType);
        }
    }
}
