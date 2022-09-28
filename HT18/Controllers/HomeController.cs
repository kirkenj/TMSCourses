using _15.Models.Classes;
using _15.Models.Enums;
using _15.Models.Structs;
using HT18.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HT18.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static Exception? currentException;
        private static readonly Parking _parking = new()
        {
            Cars = new[] {new Car(Fuel.Disel, 2000, 3000, "Mercedes"), new Car(Fuel.Disel, 1000, 3000, "Toyota"), new Car(Fuel.Disel, 4000, 3000, "BMW") } 
        };

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (currentException != null)
            {
                ViewData["exception"] = currentException;
                currentException = null;
            }

            ViewData["Cars"] = _parking.Cars;
            return View();
        }

        [HttpGet]
        public IActionResult CreateCar() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCar(string identifier, Fuel fuel, int enginePower, int tankCapacity, int fuelLevel)
        {
            var car = new Car(fuel, enginePower, tankCapacity, identifier);
            car.FuelLevel = fuelLevel;
            _parking.AddCar(car);
            return Redirect("Index");
        }

        public IActionResult RemoveCar(int index)
        {
            _parking.RemoveCar(_parking.Cars[index]);
            return Redirect("Index");
        }

        [HttpGet]
        public IActionResult EditCar(int index)
        {
            ViewData["index"] = index;
            return View(_parking.Cars[index]);
        }

        [HttpPost]
        public IActionResult EditCar(int index, string identifier, Fuel fuel, int enginePower, int tankCapacity, int fuelLevel)
        {
            var strct = new CarStruct()
            {
                Engine = new EngineStruct() { Fuel = fuel, Power = enginePower },
                Identifier = identifier,
                FuelTankCapacity = tankCapacity,
                FuelLevel = fuelLevel,
            };
            _parking.EditCar(_parking.Cars[index], strct);
            return Redirect("Index");
        }

        [HttpGet]
        public IActionResult FIllCarTank(int index)
        {
            ViewData["index"] = index;
            return View(_parking.Cars[index]);
        }

        [HttpPost]
        public IActionResult FillCarTank(int index, int volume)
        {
            _parking.FillCarTank(_parking.Cars[index], volume);
            return Redirect("Index");
        }

        public IActionResult SendCarForARide(int index)
        {
            _parking.SendCarForARide(_parking.Cars[index]);
            return Redirect("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}