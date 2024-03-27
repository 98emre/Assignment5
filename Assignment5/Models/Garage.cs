using Assignment5.Enums;
using Assignment5.Interfaces;
using Assignment5.Models.Vehicles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5.Models
{
    public class Garage<T> : IEnumerable<T> where T : IVehicle
    {
        private T[] vehicles;

        public Garage(int capacity)
        {
            vehicles = new T[capacity];
            Size = 0;
            Capacity = capacity;
        }

        public int Size { get; set; }

        public int Capacity { get; set; }

        public bool Park(T vehicle)
        {
            if (Size == Capacity || IsRegistrationTaken(vehicle.RegistrationNumber))
            {
                return false;
            }

            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] == null)
                {
                    vehicles[i] = vehicle;
                    Size++;
                    return true;
                }
            }

            return false;
        }

        public bool IsRegistrationTaken(string registrationNumber)
        {
            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] != null)
                {
                    if (vehicles[i].RegistrationNumber.Equals(registrationNumber.Trim(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool UnPark(string registrationNumber)
        {
            if (FindVehicleByReg(registrationNumber) == null)
            {
                return false;
            }

            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] != null && vehicles[i].RegistrationNumber.Equals(registrationNumber.Trim().ToUpper(), StringComparison.CurrentCultureIgnoreCase))
                {
                    vehicles[i] = default(T);
                    Size--;
                    return true;
                }
            }

            return false;
        }

        public IVehicle? FindVehicleByReg(string registrationNumber)
        {
            if (Size == 0)
            {
                return null;
            }

            foreach (IVehicle vehicle in vehicles)
            {
                if (vehicle != null)
                {
                    if (vehicle.RegistrationNumber.Equals(registrationNumber.Trim(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        return vehicle;
                    }
                }
            }

            return null;
        }

        public Dictionary<string, int> ListAllVehicles()
        {
            Dictionary<string, int> vehicleTypeCount = new Dictionary<string, int>();

            foreach (var vehicle in vehicles)
            {
                if (vehicle != null)
                {
                    string vehicleType = vehicle.GetType().Name;

                    if (vehicleTypeCount.ContainsKey(vehicleType))
                    {
                        vehicleTypeCount[vehicleType]++;
                    }
                    else
                    {
                        vehicleTypeCount[vehicleType] = 1;
                    }
                }
            }

            return vehicleTypeCount;
        }

        public List<IVehicle> SearchVehiclesByCriteria(string? color = null, int? numberOfWheels = null, VehicleType? vehicleType = null)
        {
            List<IVehicle> matchingVehicles = new List<IVehicle>();

            foreach (var vehicle in vehicles)
            {
                if(vehicle != null)
                {
                    if ((color == null || vehicle.Color.Equals(color, StringComparison.OrdinalIgnoreCase)) &&
                    (!numberOfWheels.HasValue || vehicle.NrOfWheels == numberOfWheels.Value) &&
                    (!vehicleType.HasValue || vehicle.Type == vehicleType.Value))
                    {
                        matchingVehicles.Add(vehicle);
                    }
                }
            }

            return matchingVehicles;
        }


        public IEnumerator<T> GetEnumerator()
        {
            foreach (var vehicle in vehicles)
            {
                if (vehicle != null)
                {
                    yield return vehicle;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
