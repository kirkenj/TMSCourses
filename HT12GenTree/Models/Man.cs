namespace HT12GenTree.Models
{
    public class Man : Adult
    {
        public Man(string? name, Date birthDay, Man? father, Woman? mother) : base(name, true, birthDay, father, mother)
        {
        }

        public Man(Child child) : base(child.Name, true, child.BirthDate, child.Father, child.Mother)
        {
            if (!child.IsMan)
            {
                throw new ArgumentException("It's a girl!", nameof(child));
            }
        }
    }
}
