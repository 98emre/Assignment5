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
    public class GarageHandler : IHandler
    {

        private readonly Garage<Vehicle> garage;

        public GarageHandler(int capacity)
        {
            garage = new Garage<Vehicle>(capacity);
        }

        public Garage<Vehicle> Garage { get { return garage; } }

        public Vehicle? FindVehicleByReg(string registrationNumber) => Garage.FindVehicleByReg(registrationNumber);

        public int GarageCapacity() => Garage.Capacity;

        public int NumberOfVehicles() => Garage.Size;

        public bool IsFull() => Garage.Size == Garage.Capacity;

        public bool IsRegistrationTaken(string registrationNumber) => Garage.IsRegistrationTaken(registrationNumber);

        public Dictionary<string, int> ListAllVehicles() => Garage.ListAllVehicles();

        public bool Park(Vehicle vehicle) => Garage.Park(vehicle);

        public bool UnPark(string registrationNumber) => Garage.UnPark(registrationNumber);

        public List<Vehicle> SearchVehiclesByCriteria(string? color = null, int? numberOfWheels = null, VehicleType? vehicleType = null)
        {
            return Garage.SearchVehiclesByCriteria(color,numberOfWheels,vehicleType);
        }
    }
}
