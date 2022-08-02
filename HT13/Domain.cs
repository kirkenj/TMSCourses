using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT13
{
    public static class Domain
    {
        public static string Name { get; set; }

        public static virtual override string ToString() => $"Domain - {Name}";

        public Domain(string name)
        {
            Name = name;
        }
    }
}
