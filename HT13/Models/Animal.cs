using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT13.Models
{
    internal class Animal
    {
        Species Species { get; }
        string Name { get; set; }

        public Animal(string name, Species species)
        {
            this.Species = species;
            Name = name;
        }

        public override string ToString()
        {
            return Species.ToString() + $"Animal name - {Name}.";
        }
    }
}
