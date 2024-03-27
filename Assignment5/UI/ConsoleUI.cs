using Assignment5.Enums;
using Assignment5.Handlers;
using Assignment5.Interfaces;
using Assignment5.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assignment5.UI
{
    public class ConsoleUI : IUI
    {

        private IGarageHandler? garageHandler;

        public string InputRegNr { get; set; }
        public string InputColor { get; set; }
        public int InputNrOfWheels { get; set; }

        public void Start()
        {
            GarageMenu();
        }

        public void GarageMenu()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nGarage Menu");
                Console.WriteLine("------------------------------");
                Console.WriteLine("0.Quit\n1.Create Garage ");
                Console.Write("Choose: ");
                string input = Console.ReadLine()!.Trim();

                switch (input)
                {
                    case "0":
                        running = false;
                        break;

                    case "1":
                        Console.Write("\nAdd the capacity of the garage: ");
                        input = Console.ReadLine()!.Trim();

                        int capacity;

                        if (int.TryParse(input, out capacity) && capacity > 0)
                        {
                            garageHandler = new GarageHandler(capacity);
                            Console.WriteLine($"Garage created with capacity: {capacity}");
                            ShowMenu();
                        }

                        else
                        {
                            Console.WriteLine("Invalid input for capacity, must be a number larger than 0");
                        }

                        break;

                    default:
                        Console.WriteLine("Invalid input in garage menu, choose between 0-1");
                        break;

                }
            }

        }

        public void ShowMenu()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nMain Menu");
                Console.WriteLine("------------------------------");
                Console.WriteLine("0.Quit");
                Console.WriteLine("1.Park a Vehicle");
                Console.WriteLine("2.Unpark a Vehicle");
                Console.WriteLine("3.List All Parked Vehicles");
                Console.WriteLine("4.Find a Vehicle by Registration Number");
                Console.WriteLine("5.Seach Vehicle by critiea");

                Console.Write("Choose: ");
                string input = Console.ReadLine()!.Trim();


                switch (input)
                {
                    case "0":
                        Console.WriteLine("Exiting the main menu system");
                        running = false;
                        break;

                    case "1":
                        ParkVehicle();
                        break;

                    case "2":
                        UnParkVehicle();
                        break;

                    case "3":
                        ListAllVehicles();
                        break;

                    case "4":
                        FindVehicleByReg();
                        break;

                    case "5":
                        SearchVehiclesByCriteria(); 
                        break;

                    default:
                        Console.WriteLine("Invalid input main menu, choose between 0-5 and try again");
                        break;
                }
            }
        }

        public void UnParkVehicle()
        {
            if (garageHandler?.NumberOfVehicles() == 0)
            {
                Console.WriteLine("Garage is empty");
                return;
            }

            Console.Write("\nSeach vehicle by registration number, that you want to unpark\nWrite registration number: ");
            string input = Console.ReadLine()!.Trim().ToUpper();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Input can not be empty");
                return;
            }

            Console.WriteLine(garageHandler!.UnPark(input) ? $"Vehicle ({input}) was unpark" : $"Vehicle ({input}) does not exist");
        }

        public void ListAllVehicles()
        {
            Console.WriteLine($"\nGarage Capacity:  {garageHandler!.GarageCapacity()}\nNumber of vehicles in the garage: {garageHandler.NumberOfVehicles()}");

            if (garageHandler?.NumberOfVehicles() == 0)
                Console.WriteLine("\nThere is no vehicles in the garage");

            else
            {
                Console.WriteLine("\nParked Vehicles\n--------------------------------");

                foreach (var key in garageHandler!.ListAllVehicles())
                    Console.WriteLine($"{key.Key}: ({key.Value}) parked");
            }
        }

        public void FindVehicleByReg()
        {
            if (garageHandler?.NumberOfVehicles() == 0)
            {
                Console.WriteLine("Can't search for vehicles by registration number, the garage is empty");
                return;
            }

            Console.Write("\nSearch vehicle by registration number\nWrite registration number: ");
            string input = Console.ReadLine()!.Trim().ToUpper();

            IVehicle? vehicle = garageHandler?.FindVehicleByReg(input);

            Console.WriteLine(vehicle == null ? $"Vehicle with regitration number ({input}) does not exist" : $"{vehicle}");
        }

        // Start Serach By Critiera
        
        public void SearchVehiclesByCriteria()
        {
            if (garageHandler?.NumberOfVehicles() == 0)
            {
                Console.WriteLine("Can't search for vehicles, the garage is empty");
                return;
            }

            do
            {
                Console.WriteLine("\nSearch Vehicle by Criteria");
                Console.Write("\n0.Exit\n1.Color\n2.Number of Wheels\n3.Vehicle Type\nChoose: ");
                string? input = Console.ReadLine()?.Trim();

                switch (input)
                {
                    case "0":
                        Console.WriteLine("Exit search menu");
                        return;

                    case "1":
                        SearchByColor();
                        return;
                    
                    case "2":
                        SearchByNumberOfWheels();
                        return;

                    case "3":
                        SearchByVehicleType();
                        return;

                    default:
                        Console.WriteLine("Invalid input for choosing criteria, please try again and choose between 0-3");
                        break;
                }
            } while (true);
        }

        private void SearchByColor()
        {
            do
            {
                Console.Write("Enter Color: ");
                InputColor = Console.ReadLine()!.Trim().ToUpper();

                if (string.IsNullOrEmpty(InputColor))
                {
                    Console.WriteLine("Color can't be empty, try again");
                }

            } while (string.IsNullOrEmpty(InputColor));

            List<IVehicle>? searchResult = garageHandler?.SearchVehiclesByCriteria(color: InputColor);
            DisplaySearchResult(searchResult);
        }

        private void SearchByNumberOfWheels()
        {
            Console.Write("Enter number: ");
            int nrOfWheels;

            while (!int.TryParse(Console.ReadLine()!.Trim(), out nrOfWheels) || nrOfWheels < 0)
            {
                Console.Write("Invalid input for number of wheels (must be an integer). Please try again with a valid number: ");
            }

            InputNrOfWheels = nrOfWheels;

            List<IVehicle>? searchResult = garageHandler?.SearchVehiclesByCriteria(numberOfWheels: InputNrOfWheels);
            DisplaySearchResult(searchResult);
        }

        private void SearchByVehicleType()
        {
            List<IVehicle>? searchResult = new List<IVehicle>();
            bool running = true;

            while (running) 
            {
                Console.WriteLine("\nSeach by vehicle types");
                Console.WriteLine("0.Exit\n1.Airplane\n2.Boat\n3.Bus\n4.Car\n5.Motorcycle");
                Console.Write("Choose: ");
                string input = Console.ReadLine()!.Trim();

                switch (input)
                {
                    case "0":
                        Console.WriteLine("Exit search system in vehicle types");
                        return;

                    case "1":
                        searchResult = garageHandler?.SearchVehiclesByCriteria(vehicleType: VehicleType.Airplane);
                        running = false;
                        break;


                    case "2":
                        searchResult = garageHandler?.SearchVehiclesByCriteria(vehicleType: VehicleType.Boat);
                        running = false;
                        break;

                    case "3":
                        searchResult = garageHandler?.SearchVehiclesByCriteria(vehicleType: VehicleType.Bus);
                        running = false;
                        break;

                    case "4":
                        searchResult = garageHandler?.SearchVehiclesByCriteria(vehicleType: VehicleType.Car);
                        running = false;
                        break;

                    case "5":
                        searchResult = garageHandler?.SearchVehiclesByCriteria(vehicleType: VehicleType.Motorcycle);
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid input for choosing vehicle type, choose between 0-5 and try again");
                        break;
                }
            } 
            
            DisplaySearchResult(searchResult);
        }
        
        private void DisplaySearchResult(List<IVehicle>? searchResult)
        {
            if (searchResult != null && searchResult.Count != 0)
            {
                Console.WriteLine("\nSearch Results");

                foreach (var vehicle in searchResult)
                {
                    Console.WriteLine(vehicle);
                }
            }

            else
            {
                Console.WriteLine("\nNo vehicles found matching the criteria.");
            }
        }

        // End Serach By Critiera 


        // Start Park Vehicle

        public void ParkVehicle()
        {
            bool running = true;

            if (garageHandler!.IsFull())
            {
                Console.WriteLine("Garage is full");
                return;
            }

            while (running)
            {
                Console.WriteLine("\nPark a vehicle");
                Console.Write("0.Exit\n1.Airplane\n2.Boat\n3.Bus\n4 Car\n5.Motorcycle\nChoose: ");
                string input = Console.ReadLine()!.Trim();

                switch (input)
                {
                    case "0":
                        Console.WriteLine("Exit park system");
                        running = false;
                        break;

                    case "1":
                        ParkAirplane(VehicleType.Airplane);
                        running = false;
                        break;


                    case "2":
                        ParkBoat(VehicleType.Boat);
                        running = false;
                        break;

                    case "3":
                        ParkBus(VehicleType.Bus);
                        running = false;
                        break;

                    case "4":
                        ParkCar(VehicleType.Car);
                        running = false;
                        break;

                    case "5":
                        ParkMotorcycle(VehicleType.Motorcycle);
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid input for choosing vehicle, choose between 0-5 and try again");
                        break;
                }

            }

        }

        private void GetVehicleInput(VehicleType vehicleType)
        {
            Console.WriteLine($"\nAdd a {vehicleType} to the parking\n");

            do
            {
                Console.Write("Registration Number: ");
                InputRegNr = Console.ReadLine()!.Trim().ToUpper();

                if (garageHandler!.IsRegistrationTaken(InputRegNr))
                {
                    Console.WriteLine($"Registration number {InputRegNr} is alreday taken, Please try again");
                }

                if (string.IsNullOrEmpty(InputRegNr))
                {
                    Console.WriteLine("Registration Number can't be empty, try agian please");
                }

            } while (garageHandler!.IsRegistrationTaken(InputRegNr) || string.IsNullOrEmpty(InputRegNr));

            do
            {
                Console.Write("Color: ");
                InputColor = Console.ReadLine()!.Trim().ToUpper();

                if (string.IsNullOrEmpty(InputColor))
                {
                    Console.WriteLine("Color can't be empty");
                }

            } while (string.IsNullOrEmpty(InputColor));


            Console.Write("Number of wheels: ");
            int nrOfWheels;

            while (!int.TryParse(Console.ReadLine()!.Trim(), out nrOfWheels) || nrOfWheels < 0)
            {
                Console.Write("Invalid input for number of wheels (must be an integer). Please try again with a valid number: ");
            }

            InputNrOfWheels = nrOfWheels;
        }

        private void ParkAirplane(VehicleType vehicleType)
        {
            GetVehicleInput(vehicleType);

            Console.Write("Number of engines: ");
            int engines;

            while (!int.TryParse(Console.ReadLine()!.Trim(), out engines) || engines < 0)
                Console.Write("Invalid input for number of engines. Please try again with a valid number: ");


            Airplane airplane = new Airplane(InputRegNr, InputColor, InputNrOfWheels, engines);
            garageHandler?.Park(airplane);

            Console.WriteLine($"You parked ({airplane}) successfully");
        }

        private void ParkBoat(VehicleType vehicleType)
        {
            GetVehicleInput(vehicleType);

            double length;
            string? input;

            do
            {
                Console.Write("Add the length: ");
                input = Console.ReadLine()?.Trim();

                if (!double.TryParse(input, out length) || length < 0.0 || input?.Count(c => c == '.') != 1)
                     Console.WriteLine("Invalid input for length (must be double vaule - decimal). Please try again: ");


            } while (length < 0.0 || input?.Count(c => c == '.') != 1);


            Boat boat = new Boat(InputRegNr, InputColor, InputNrOfWheels, length);
            garageHandler?.Park(boat);

            Console.WriteLine($"You parked ({boat}) successfully");

        }

        private void ParkBus(VehicleType vehicleType)
        {
            GetVehicleInput(vehicleType);

            Console.Write("Add the number of seats: ");
            int numberOfseats;

            while (!int.TryParse(Console.ReadLine()!.Trim(), out numberOfseats) || numberOfseats < 0)
                Console.Write("Invalid input for number of seats (must be Integer). Please try again with a valid number: ");


            Bus bus = new Bus(InputRegNr, InputColor, InputNrOfWheels, numberOfseats);
            garageHandler?.Park(bus);

            Console.WriteLine($"You parked ({bus}) successfully");
        }

        private void ParkCar(VehicleType vehicleType)
        {
            GetVehicleInput(vehicleType);

            FuelType fuelType;

            do
            {
                Console.Write("\nFuel types\n1. Gasoline\n2. Diesel\n3. Electricity\nChoose: ");
                string input = Console.ReadLine()!.Trim();

                if (int.TryParse(input, out int choice) && Enum.IsDefined(typeof(FuelType), choice))
                {
                    fuelType = (FuelType)choice;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input for fuel type, please choose between 1-3");
                }

            } while (true);


            Car car = new Car(InputRegNr, InputColor, InputNrOfWheels, fuelType);
            garageHandler?.Park(car);

            Console.WriteLine($"You parked ({car}) successfully");
        }

        private void ParkMotorcycle(VehicleType vehicleType)
        {
            GetVehicleInput(vehicleType);

            Console.Write("Add cylinder volume: ");
            int cylinderVolume;

            while (!int.TryParse(Console.ReadLine()!.Trim(), out cylinderVolume) || cylinderVolume < 0)
            {
                Console.Write("Invalid input for cylinder volume (must be Integer). Please try again with a valid number: ");
            }

            Motorcycle motorcycle = new Motorcycle(InputRegNr, InputColor, InputNrOfWheels, cylinderVolume);
            garageHandler?.Park(motorcycle);

            Console.WriteLine($"You parked ({motorcycle}) successfully");
        }

        // End Park Vehicle

    }
}
