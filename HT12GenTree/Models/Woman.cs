using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT12GenTree.Models
{
    public class Woman : Adult
    {
        public Woman(string? name, Date birthDay, Man? father, Woman? mother) : base(name, false, birthDay, father, mother)
        {
        }

        public Woman(Child child) : base(child.Name, false, child.BirthDate, child.Father, child.Mother) 
        {
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
