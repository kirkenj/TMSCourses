﻿using _15.Models.Enums;
using _15.Models.Interfaces;
using _15.Models.Structs;
using _15.Models.Exceptions;

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
                    throw new FuelException("Value can not be less 0",nameof(value));
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
        public Fuel Fuel => Engine?.Fuel ?? throw new MissingValueException(nameof(Engine));
        public int EnginePower => Engine?.Power ?? throw new MissingValueException(nameof(Engine));

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
                throw new MissingValueException(nameof(Engine));
            }

            if (fuel != this.Engine.Fuel)
            {
                throw new FuelException("Invalid Fuel");
            }

            if (fuelVolume < 0)
            {
                throw new FuelException("Value can not be less 0", nameof(fuelVolume));
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
                throw new MissingValueException(nameof(Engine), "Car can't ride without an engine");
            }

            if (FuelLevel == 0)
            {
                throw new FuelException(nameof(FuelLevel), "Car can't ride not having fuel in the tank");
            }

            FuelLevel -= _random.Next(0, FuelLevel);
        }

        public Car() { }

        public Car(Fuel fuel, int enginePower, int tankCapacity, string identifier, int fuelLevel = 0) : this(new Engine(fuel, enginePower), tankCapacity, identifier, fuelLevel) { }

        public Car(CarStruct? carStruct) 
        {
            if (carStruct == null)
            {
                throw new MissingValueException(nameof(carStruct));
            }

            Engine = new Engine(carStruct.Value.Fuel, carStruct.Value.EnginePower);
            FuelTankCapacity = carStruct.Value.FuelTankCapacity;
            Identifier = carStruct.Value.Identifier;
            FuelLevel = carStruct.Value.FuelLevel;
        }

        public Car(Engine engine, int tankCapacity, string identifier, int fuelLevel = 0)
        {
            Engine = engine ?? throw new MissingValueException(nameof(engine));
            if (tankCapacity < 0)
            {
                throw new InvalidValueException(nameof(tankCapacity), "Tank capacity can not be less 0");
            }

            FuelTankCapacity = tankCapacity;
            if (string.IsNullOrWhiteSpace(identifier))
            {
                throw new MissingValueException(nameof(identifier));
            }

            FuelLevel = fuelLevel;
            Identifier = identifier.Trim();
        }

        public void Edit(Engine engine, int tankCapacity, string identifier, int fuelLevel = 0)
        {
            Engine = engine ?? throw new MissingValueException(nameof(engine));
            if (tankCapacity < 0)
            {
                throw new InvalidValueException(nameof(tankCapacity), "Tank capacity can not be less 0");
            }

            FuelTankCapacity = tankCapacity;
            if (string.IsNullOrWhiteSpace(identifier))
            {
                throw new MissingValueException(nameof(identifier));
            }

            Identifier = identifier.Trim();
            FuelLevel = fuelLevel;
        }

        public void Edit(Fuel fuel, int enginePower, int tankCapacity, string identifier, int fuelLevel = 0) => Edit(new Engine(fuel, enginePower), tankCapacity, identifier, fuelLevel);
        public void Edit(CarStruct? carStruct)
        {
            if (carStruct == null) 
            {
                throw new MissingValueException(nameof(carStruct)); 
            }

            Edit(new Engine(carStruct.Value.Fuel, carStruct.Value.EnginePower), carStruct.Value.FuelTankCapacity, carStruct.Value.Identifier, carStruct.Value.FuelLevel);
        }
        public CarStruct GetStruct() => new() { Engine = Engine?.GetStruct() ?? throw new MissingValueException(nameof(Engine)), FuelLevel = FuelLevel, FuelTankCapacity = FuelTankCapacity, Identifier = Identifier }; 
        public override string ToString() => $"Identifier: \"{Identifier}\", {Engine ?? throw new MissingValueException(nameof(Engine))}, Fuel tank capacity: {FuelTankCapacity}, Fuel level: {FuelLevel}";
    }
}
