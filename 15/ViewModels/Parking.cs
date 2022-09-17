using _15.Models.Classes;
using _15.Models.Enums;
using static testRepo.Programm;

namespace _15.ViewModels
{
    internal class Parking
    {
        private readonly List<Car> _cars = new();
        private readonly Dictionary<ParkingMenu, UserAction> dict = new();
        private readonly Stack<string> loggs = new();
        private event MessagePrinting Notify;
        public Parking()
        {
            Notify += Console.WriteLine;
            Notify += loggs.Push;
            dict.Add(ParkingMenu.AddCar, AddCar);
            dict.Add(ParkingMenu.EditCar, EditCar);
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
            dict.Add(ParkingMenu.PrintLogs, () => Console.WriteLine(string.Join("\n", loggs)));


            _cars.Add(new Car(Fuel.Petrol, 3000, 500, "1020 BH-4"));
            _cars.Add(new Car(Fuel.Disel, 2000, 900, "1050 AM-4"));
            _cars.Add(new Car(Fuel.Electricity, 8000, 1000, "8999 QF-4"));
            Notify(DateTime.Now.ToString() + $": Parking was creeated with:\n{string.Join("\n", _cars)}");
        }


        public void AddCar()
        {
            Car? car = Car.CreateCarByUser();
            if (car != null)
            {
                Notify(DateTime.Now.ToString() + $": Car added:{car}");
                _cars.Add(car);
            }
        }

        public void EditCar()
        {
            var car = SeletctItemFromArray("Select a car", _cars.ToArray());

            if (car == default)
            {
                return;
            }

            string prevStr = car.ToString();
            if (car.EditByUser())
            {
                Notify(DateTime.Now.ToString() + $": Car: {prevStr}\nwas edited and got values:\n{car}");
            }
        }

        public void RemoveCar()
        {
            var car = SeletctItemFromArray("Select car to remove", _cars.ToArray());
            if (car != null)
            {
                _cars.Remove(car);
                Notify(DateTime.Now.ToString() + $":Car removed:{car}");
            }
        }

        public void SendCarForARide()
        {
            var car = SeletctItemFromArray("Select a car", _cars.ToArray());

            if (car == default)
            {
                return;
            }

            var prevLevel = car.FuelLevel;
            car.Ride();
            Notify(DateTime.Now.ToString() + $": Car (ID = {car.ID}) was sent for a ride. Car's fuel level reduced from {prevLevel} to {car.FuelLevel}");
        }

        public void FillCarTank()
        {
            var car = SeletctItemFromArray("Select a car", _cars.ToArray());

            if (car == default)
            {
                return;
            }

            var volume = ReadIntFromConsole("Input fuel volume", 0, int.MaxValue);
            var extraFuel = car.FillTank(car.Fuel, volume);
            Notify(DateTime.Now.ToString() + $": Car: {car}. Tank filled with {volume} fuel" + (extraFuel > 0 ? $"Extra fuel: {extraFuel}" : string.Empty));
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

    delegate void UserAction();
    delegate void MessagePrinting(string msg);
}
