using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT12GenTree.Models
{
    public class Man : Adult
    {
        public Man(string? name, Date birthDay, Man? father, Woman? mother) : base(name, true, birthDay, father, mother)
        {
        }

        public Man(Child child) : base(child.Name, true, child.BirthDate, child.Father, child.Mother) 
        { 
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
