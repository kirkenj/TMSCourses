using System;
namespace HT12GenTree.Models
{
    public class Woman : Adult
    {
        public Woman(string? name, Date birthDay, Man? father, Woman? mother) : base(name, false, birthDay, father, mother)
        {
        }

        public Woman(Child child) : base(child.Name, false, child.BirthDate, child.Father, child.Mother) 
        {
            if (child.IsMan)
            {
                throw new ArgumentException("It's a boy!", nameof(child));
            }
        }
    }
}
