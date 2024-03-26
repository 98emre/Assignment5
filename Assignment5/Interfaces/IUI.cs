using Assignment5.Enums;
using Assignment5.Models.Vehicles;
using Assignment5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5.Interfaces
{
    public interface IUI
    {
        string InputRegNr { get; set; }
        string InputColor { get; set; }

        int InputNrOfWheels { get; set; }

        void Start();

        void GarageMenu();
        void ShowMenu();

        void ParkVehicle();
        void UnParkVehicle();

        void ListAllVehicles();
        void FindVehicleByReg();
        void SearchVehiclesByCriteria();
    }
}
