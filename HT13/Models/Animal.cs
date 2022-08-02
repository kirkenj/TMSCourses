using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT13.Models
{
    internal class Animal
    {
        Species species { get; }
        string Name { get; set; }

        public Animal(string name, Species species)
        {
            this.species = species;
            Name = name;
        }

        public override string ToString()
        {
            return species.ToString() + $"Animal name - {Name}.";
        }
    }
}
