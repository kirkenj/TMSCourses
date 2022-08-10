using _15.Models.Enums;
using _15.Models.Interfaces;

namespace _15.Models.Classes
{
    internal class Car : ICar<IEngine>, ICloneable, IComparable
    {
        private static readonly Random _random = new();
        public IEngine? Engine { get; private set; }

        public int FuelTankCapacity {get; private set; }

        public string Identifier { get; private set; } = string.Empty;

        public int FuelLevel { get; private set; }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object? obj)
        {
            throw new NotImplementedException();
        }

        public int FillTank(Fuel fuel, int fuelVolume)
        {
            if (Engine == null)
            {
                throw new Exception("Car doesn't have an engine");
            }

            if (fuel != this.Engine.Fuel)
            {
                throw new Exception("Invalid Fuel");
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
                throw new Exception("Car can't ride without an engine");
            }

            if (FuelLevel == 0)
            {
                throw new Exception("Car can't ride not having fuel in the tank");
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


    }
}
