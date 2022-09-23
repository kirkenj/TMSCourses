using _15.Models.Classes;
using _15.Models.Enums;
using _15.Models.Structs;
using System.Text;
using static testRepo.Programm;

namespace _15.ViewModels
{
    public class MainMenu
    {
        public readonly Parking parking;
        private readonly Dictionary<ParkingMenu, Action> dict = new();
        private static readonly Fuel[] _fuels = Enum.GetValues(typeof(Fuel)).Cast<Fuel>().ToArray();
        readonly FileStream loggingFileStream;
        public string defaultJsonPath = "parking.txt";
        public string defaultXMLPath = "parkingXml.txt";

        public static void PrintMessage(CarStruct? carPrev, Car? carCur)
        {
            Console.WriteLine($"{(carPrev?.ToString() ?? "Car creation")}\nto\n{carCur?.ToString() ?? "null"}");
        }
        public void PrintMessageIntoLogs(CarStruct? carPrev, Car? carCur)
        {
            var msg = $"{DateTime.Now}: {(carPrev?.ToString() ?? "Car creation")} to {carCur?.ToString() ?? "null"}\n";
            var bytes = Encoding.ASCII.GetBytes(msg);
            loggingFileStream.Position = loggingFileStream.Length;
            foreach(var aByte in bytes)
            {
                loggingFileStream.WriteByte(aByte);
            }
        }

        internal void Start()
        {
            ParkingMenu option;
            while (true)
            { 
                try
                {
                    option = SeletctItemFromArray("Select option", dict.Keys.ToArray());
                    if (option == default)
                    {
                        break;
                    }

                    dict[option]();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public MainMenu(Parking parking, FileStream logFile)
        {
            this.parking = parking ?? throw new ArgumentNullException(nameof(parking));
            this.loggingFileStream = logFile ?? throw new ArgumentNullException(nameof(logFile));

            parking.CarValueChangedEvent += PrintMessage;
            parking.CarValueChangedEvent += PrintMessageIntoLogs;
            dict.Add(ParkingMenu.AddCar, AddCar);
            dict.Add(ParkingMenu.EditCar, EditCar);
            dict.Add(ParkingMenu.FillCarTank, FillCarTank);
            dict.Add(ParkingMenu.RemoveCar, RemoveCar);
            dict.Add(ParkingMenu.SendCarForARide, SendCarForARide);
            dict.Add
            (
                ParkingMenu.SortCarsByPower, () =>
                {
                    Console.WriteLine(String.Join("\n", parking.Cars.OrderBy(c => c.EnginePower)));
                }
            );
            dict.Add(ParkingMenu.SortCarsByDefault, () =>
            {
                if (!parking.Cars.Any())
                {
                    return;
                }

                var arr = parking.Cars.ToArray();
                Array.Sort(arr);
                Console.WriteLine(String.Join("\n", arr.ToList()));
            });
            dict.Add(ParkingMenu.PrintCars, () => Console.WriteLine(String.Join("\n", parking.Cars.ToList())));
            dict.Add(ParkingMenu.PrintLogs, PringLogs);
            dict.Add(ParkingMenu.SerializeJson, SerializeJson);
            dict.Add(ParkingMenu.SerializeXML, SerializeXML);
        }

        private void SerializeXML()
        {
            string? path;
            if (YesNoSelection("Use default path?"))
            {
                path = defaultXMLPath;
            }
            else
            {
                Console.WriteLine("Input file's save path:");
                path = Console.ReadLine();
            }

            if (path == null)
            {
                Console.WriteLine("Invalid path");
                return;
            }

            using FileStream file = new(path, FileMode.OpenOrCreate);
            parking.SerializeXML(file);
        }

        private void SerializeJson()
        {
            string? path;
            if (YesNoSelection("Use default path?"))
            {
                path = defaultJsonPath;
            }
            else
            {
                Console.WriteLine("Input file's save path:");
                path = Console.ReadLine();
            }

            if (path == null)
            {
                Console.WriteLine("Invalid path");
                return;
            }

            using FileStream file = new(path, FileMode.OpenOrCreate);
            parking.SerializeJson(file);
        }

        private void PringLogs()
        {
            byte[] arr = new byte[loggingFileStream.Length];
            loggingFileStream.Position = 0;
            loggingFileStream.Read(arr);
            Console.WriteLine(Encoding.ASCII.GetString(arr));
        }

        private void FillCarTank()
        {
            var car = SeletctItemFromArray("Select a car", parking.Cars.ToArray());
            if (car == null)
            {
                return;
            }

            var fuel = SeletctItemFromArray("Select fuel", _fuels);
            if (fuel == default)
            {
                return;
            }

            var volume = ReadIntFromConsole("Input fuel volume", 0, int.MaxValue);
            parking.FillCarTank(car, volume);
        }

        private void SendCarForARide()
        {
            var car = SeletctItemFromArray("Select a car", parking.Cars.ToArray());
            if (car == null)
            {
                return;
            }

            parking.SendCarForARide(car);
        }

        private void RemoveCar()
        {
            var car = SeletctItemFromArray("Select a car", parking.Cars.ToArray());
            if (car == null)
            {
                return;
            }

            parking.RemoveCar(car);
        }

        private void EditCar() 
        {
            var car = SeletctItemFromArray("Select a car", parking.Cars.ToArray());
            if (car == null)
            {
                return;
            }

            var values = InputCarValues();
            if (!values.Equals(default)) {
                parking.EditCar(car, values);
            }
        }

        public static CarStruct? InputCarValues()
        {
            var fuel = SeletctItemFromArray("Select fuel", _fuels);
            if (fuel == default)
            {
                return null;
            }

            var power = ReadIntFromConsole("Input engine's power", 0, int.MaxValue);
            var capacity = ReadIntFromConsole("Input tank's capacity", 0, int.MaxValue);
            Console.WriteLine("Input car's identifier");
            var identifier = Console.ReadLine() ?? string.Empty;
            var car = new CarStruct() { Engine = new EngineStruct() { Fuel = fuel, Power = power}, FuelTankCapacity = capacity, Identifier = identifier };
            return car;
        }

        private void AddCar()
        {
            var values = InputCarValues();
            if (values == null)
            {
                return;
            }

            var car = new Car(values);
            parking.AddCar(car);
        }
    }
}
