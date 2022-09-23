using _15.Models.Enums;
using _15.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15.Models.Structs
{
    public struct CarStruct 
    {
        public Fuel Fuel => Engine?.Fuel ?? throw new ArgumentNullException(nameof(Engine));
        public int EnginePower => Engine?.Power ?? throw new ArgumentNullException(nameof(Engine));
        public IEngine Engine { get; set; }
        public int FuelTankCapacity { get; set; }
        public string Identifier { get; set; }
        public int FuelLevel { get; set; }

        public override string ToString() => $"Identifier: \"{Identifier}\", {(Engine == null ? "Invalid engine" : Engine.ToString())}, Fuel tank capacity: {FuelTankCapacity}, Fuel level: {FuelLevel}";
    }
}
