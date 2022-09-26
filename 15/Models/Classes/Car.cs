﻿using _15.Models.Enums;
using _15.Models.Interfaces;
using _15.Models.Structs;

namespace _15.Models.Classes
{
    [Serializable]
    public class Car :  ICar<Engine>, ICloneable, IComparable
    {
        private static readonly Random _random = new();
        public Engine? Engine { get; set; } = default;
        public int FuelTankCapacity { get; set; } = 0;
        public string Identifier { get; set; } = string.Empty;
        public int FuelLevel 
        { 
            get => _fuelLevel; 
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentException(nameof(value));
                }

                if (value > FuelTankCapacity) 
                {
                    _fuelLevel = FuelTankCapacity;
                }
                else
                {
                    _fuelLevel = value;
                }
            } 
        }
        private int _fuelLevel = 0;
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

        public Car(CarStruct? carStruct) 
        {
            if (carStruct == null)
            {
                throw new ArgumentNullException(nameof(carStruct));
            }

            Engine = new Engine(carStruct.Value.Fuel, carStruct.Value.EnginePower);
            FuelTankCapacity = carStruct.Value.FuelTankCapacity;
            Identifier = carStruct.Value.Identifier;
        }

        public Car(Engine engine, int tankCapacity, string identifier)
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

            FuelLevel = 0;
            Identifier = identifier.Trim();
        }

        public void Edit(Engine engine, int tankCapacity, string identifier, int fuelLevel = 0)
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
            FuelLevel = fuelLevel;
        }

        public void Edit(Fuel fuel, int enginePower, int tankCapacity, string identifier, int fuelLevel = 0) => Edit(new Engine(fuel, enginePower), tankCapacity, identifier, fuelLevel);
        public void Edit(CarStruct? carStruct)
        {
            if (carStruct == null) 
            {
                throw new ArgumentNullException(nameof(carStruct)); 
            }

            Edit(new Engine(carStruct.Value.Fuel, carStruct.Value.EnginePower), carStruct.Value.FuelTankCapacity, carStruct.Value.Identifier, carStruct.Value.FuelLevel);
        }
        public CarStruct GetStruct() => new() { Engine = Engine?.GetStruct() ?? throw new ArgumentNullException(nameof(Engine)), FuelLevel = FuelLevel, FuelTankCapacity = FuelTankCapacity, Identifier = Identifier }; 
        public override string ToString() => $"Identifier: \"{Identifier}\", {Engine ?? throw new ArgumentNullException(nameof(Engine))}, Fuel tank capacity: {FuelTankCapacity}, Fuel level: {FuelLevel}";
    }
}
