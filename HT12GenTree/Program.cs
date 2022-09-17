using HT12GenTree.Models;

namespace HT12GenTree 
{
    public class Program
    {
        public static void PrintTree(Child? child)
        {
            if (child == null)
            {
                return;
            }

            Console.WriteLine(child.ToString());
            PrintTree(child.Father);
            PrintTree(child.Mother);
        }

        public static void Main()
        {
            var Grandad1 = new Man("Vitovt", new Date(12, 3, 1970), null, null);
            var Grandad2 = new Man("Vitold", new Date(12, 3, 1972), null, null);
            var Grandmom1 = new Woman("Mary", new Date(12, 3, 1975), null, null);
            var Grandmom2 = new Woman("Mary", new Date(12, 3, 1973), null, null);
            var Father = new Man("Misha", new Date(12, 3, 1990), Grandad2, Grandmom2);
            var Mom = new Woman("Masha", new Date(12, 3, 1992), Grandad1, Grandmom1);
            var Child2 = new Child("Kate", false, new Date(12, 3, 2010), Father, Mom);
            PrintTree(Child2);
        }
    }
}

