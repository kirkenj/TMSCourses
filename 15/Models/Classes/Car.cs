using _15.Models.Enums;
using _15.Models.Interfaces;
using static testRepo.Programm;


namespace _15.Models.Classes
{
    internal class Car : ICar<IEngine>, ICloneable, IComparable
    {
        private static readonly Random _random = new();
        public IEngine? Engine { get; private set; }

        public int FuelTankCapacity { get; private set; }

        public string Identifier { get; private set; } = string.Empty;

        public int FuelLevel { get; private set; }

        public Fuel Fuel => Engine?.Fuel ?? throw new ArgumentNullException(nameof(Engine));

        public int EnginePower => Engine?.Power ?? throw new ArgumentNullException(nameof(Engine));

        public object Clone() => new Car(Fuel, EnginePower, FuelTankCapacity, Identifier);
        
        public int CompareTo(object? obj)
        {
            if (obj is Car car) return Identifier.CompareTo(car.Identifier);
            else throw new ArgumentException("Incorrect parameter");
        }

        public int FillTank(Fuel fuel, int fuelVolume)
        {
            if (Engine == null)
            {
                throw new Exception("Car doesn't have an engine");
            }

            if (fuel != this.Engine.Fuel)
            {
                throw new ArgumentException("Invalid Fuel");
            }

            int freeTankVolume = FuelTankCapacity - FuelLevel;
            if (freeTankVolume < fuelVolume)
            {
                FuelLevel += fuelVolume;
                return 0;
            }

            FuelLevel = FuelTankCapacity;
            return fuelVolume - freeTankVolume;
        }

        public void Ride()
        {
            if (Engine == null)
            {
                throw new ArgumentNullException(nameof(Engine), "Car can't ride without an engine");
            }

            if (FuelLevel == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(FuelLevel), "Car can't ride not having fuel in the tank");
            }

            FuelLevel -= _random.Next(0, FuelLevel);
        }

        public Car(Fuel fuel, int enginePower, int tankCapacity, string identifier) : this(new Engine(fuel, enginePower), tankCapacity, identifier) { }

        public Car(IEngine engine, int tankCapacity, string identifier)
        {
            Engine = engine ?? throw new ArgumentNullException(nameof(engine));
            if (tankCapacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(tankCapacity));
            }

            FuelTankCapacity = tankCapacity;
            if (string.IsNullOrWhiteSpace(identifier))
            {
                throw new ArgumentNullException(nameof(identifier));
            }

            Identifier = identifier.Trim();
        }

        public override string ToString() => $"Identifier: \"{Identifier}\", {Engine}, Fuel tank capacity: {FuelTankCapacity}, Fuel level: {FuelLevel}";

        public static Car? CreateCarByUser()
        {
            var engine = Classes.Engine.CreateEngineByUser();
            if (engine == default)
            {
                return null;
            }

            var tankCapacity = ReadIntFromConsole("Input tank's capacity (liters)", 0, 1000);
            Console.WriteLine("Input car's identifier");
            string identifier = Console.ReadLine() ?? "";
            return new Car(engine, tankCapacity, identifier);
        }
    }
}
