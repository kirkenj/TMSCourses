﻿using _15.Models.Enums;
using _15.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15.Models.Structs
{
    public struct EngineStruct : IEngine
    {
        public Fuel Fuel { get; set; }
        public int Power { get; set; }

        public override string ToString() => $"Power: {Power}, Fuel: {Fuel}";
    }
}
