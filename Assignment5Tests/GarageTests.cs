using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment5.Enums;
using Assignment5.Models;
using Assignment5.Models.Vehicles;

namespace Assignment5.Tests
{
    [TestClass()]
    public class GarageTests
    {

        private Garage<Vehicle>? garage;

        [TestInitialize]
        public void Setup()
        {
            garage = new Garage<Vehicle>(3);
        }

        [TestMethod()]
        public void Park_ValidVehicle_ReturnsTrue()
        {
            Car car = new Car("ABC123", "Red", 4, FuelType.Gasoline);

            bool result = garage!.Park(car);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Park_DuplicateRegistration_ReturnsFalse()
        {
            Car car = new Car("ABC123", "Red", 4, FuelType.Gasoline);
            Car car2 = new Car("ABC123", "Black", 4, FuelType.Diesel);

            garage!.Park(car);
            bool result = garage.Park(car2);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Park_FullGarage_ReturnsFalse()
        {
            var vehicle1 = new Car("ABC123", "Red", 4, FuelType.Gasoline);
            var vehicle2 = new Car("ABC321", "Black", 4, FuelType.Diesel);
            var vehicle3 = new Airplane("ABC333", "Black", 5, 3);
            var vehicle4 = new Boat("ABC111", "White",3, 2.0);
            var vehicle5 = new Motorcycle("ABC999", "Red", 2, 5);
            var vehicle6 = new Bus("ABC555", "Yellow", 6, 35);

            garage?.Park(vehicle1);
            garage?.Park(vehicle2);
            garage?.Park(vehicle3);
            garage?.Park(vehicle4);
            garage?.Park(vehicle5);
            bool result = garage!.Park(vehicle6); 
            

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UnPark_ExistingVehicle_ReturnsTrue()
        {
            var vehicle = new Car("ABC123", "Red", 4, FuelType.Gasoline);
            garage?.Park(vehicle);

            bool result = garage!.UnPark("ABC123");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UnPark_NonExistingVehicle_ReturnsFalse()
        {
            bool result = garage!.UnPark("XYZ789");

            Assert.IsFalse(result);
        }
    }
}