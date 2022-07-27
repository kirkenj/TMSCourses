using HT12GenTree.Models;

namespace HT12GenTree 
{
    public class Program
    {
        public static void Main()
        {
            var m = new Woman("Meow", new Date(1,1,2000), null, null);
            var p = new Child("Meowa", false, Date.Now, null, m);
            Console.WriteLine(m);
            Console.WriteLine(p);
            Console.WriteLine(new String('-', 30));
            p.Mother = null;
            Console.WriteLine(m);
            Console.WriteLine(p);
            Console.WriteLine(new String('-', 30));
            p.Mother = m;
            Console.WriteLine(m);
            Console.WriteLine(p);
            Console.WriteLine(new String('-', 30));
            p.Mother = null;
            Console.WriteLine(m);
            Console.WriteLine(p);
            Console.WriteLine(new String('-', 30));
            p.Mother = m;
            Console.WriteLine(m);
            Console.WriteLine(p);
            Console.WriteLine(new String('-', 30));
            p.Mother = null;
            Console.WriteLine(m);
            Console.WriteLine(p);


        }
    }
}

