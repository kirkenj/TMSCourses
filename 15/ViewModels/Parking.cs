using _15.Models.Classes;
using _15.Models.Enums;
using static testRepo.Programm;

namespace _15.ViewModels
{
    internal class Parking
    {
        private readonly List<Car> _cars = new();
        private readonly Dictionary<ParkingMenu, Action> dict = new();

        public Parking()
        {
            dict.Add(ParkingMenu.AddCar, AddCar);
            dict.Add(ParkingMenu.RemoveCar, RemoveCar);
            dict.Add(ParkingMenu.SendCarForARide, SendCarForARide);
            dict.Add
            (
                ParkingMenu.SortCarsByPower, () =>
                {
                    Console.WriteLine(String.Join("\n", _cars.OrderBy(c => c.EnginePower)));
                }
            );
            dict.Add(ParkingMenu.SortCarsByDefault, () =>
            {
                if (!_cars.Any())
                {
                    return;
                }

                var arr = _cars.ToArray();
                Array.Sort(arr);
                Console.WriteLine(String.Join("\n", arr.ToList()));
            });
            dict.Add(ParkingMenu.PrintCars, PrintCars);
            dict.Add(ParkingMenu.FillCarTank, FillCarTank);

            _cars.Add(new Car(Fuel.Petrol, 3000, 500, "1020 BH-4"));
            _cars.Add(new Car(Fuel.Disel, 2000, 900, "1050 AM-4"));
            _cars.Add(new Car(Fuel.Electricity, 8000, 1000, "8999 QF-4"));
        }


        public void AddCar()
        {
            Car? car = Car.CreateCarByUser();
            if (car != null)
            {
                _cars.Add(car);
            }
        }

#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
        public void RemoveCar() => _cars.Remove(SeletctItemFromArray("Select car to remove", _cars.ToArray()));
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.

        public void SendCarForARide()
        {
            var car = SeletctItemFromArray("Select a car", _cars.ToArray());

            if (car == default)
            {
                return;
            }

            var prevLevel = car.FuelLevel;
            car.Ride();
            Console.WriteLine($"Car's fuel level reduced from {prevLevel} to {car.FuelLevel}");
        }

        public void FillCarTank()
        {
            var car = SeletctItemFromArray("Select a car", _cars.ToArray());

            if (car == default)
            {
                return;
            }

            var extraFuel = car.FillTank(car.Fuel, ReadIntFromConsole("Input fuel volume", 0, int.MaxValue));
            if (extraFuel > 0)
            {
                Console.WriteLine($"Extra fuel: {extraFuel}");
            }
        }

        public void PrintCars() => Console.WriteLine(string.Join("\n", _cars));

        public void Start()
        {
            while (true)
            {
                var keys = dict.Keys.ToArray();
                var option = SeletctItemFromArray("Parking menu:", keys);
                if (option == default)
                {
                    return;
                }

                dict[option]();
            }
        }
    }
}
