﻿using _15.Models.Enums;
using _15.Models.Interfaces;
using static testRepo.Programm;


namespace _15.Models.Classes
{
    [Serializable]
    internal struct Car : ICar<IEngine>, ICloneable, IComparable
    {
        private static readonly Random _random = new();

        public IEngine? Engine { get; set; } = default;

        public int FuelTankCapacity { get; set; } = 0;
        public string Identifier { get; set; } = string.Empty;

        public int FuelLevel { get; set; } = 0;
        
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
            if (freeTankVolume > fuelVolume)
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

        public Car() { }

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

        /// <summary>
        /// Return true if edition was NOT cancelled
        /// </summary>
        /// <returns></returns>
        //public bool EditByUser()
        //{
        //    var engine = Classes.Engine.CreateEngineByUser();
        //    if (engine.Equals(default))
        //    {
        //        return false;
        //    }

        //    this.Engine = engine;
        //    this.FuelTankCapacity = ReadIntFromConsole("Input tank's capacity", 0, 1000);
        //    Console.WriteLine("Input car's identifier");
        //    this.Identifier = Console.ReadLine() ?? "";
        //    this.FuelLevel = 0;
        //    return true;
        //}


        //public static Car? CreateCarByUser()
        //{
        //    var car = new Car();
        //    return car.EditByUser() ? car : default;
        //}
    }
}