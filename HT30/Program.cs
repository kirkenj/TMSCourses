namespace HT30 
{ 
    class Program
    {
        private static void GetHello()
        {
            Person person = new("Nick");
            Person person2 = new("Tom");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                person.SayHello();
            }
            else
            {
                person2.SayHello();
            }
        }

        public static async Task RunTimmy()
        {
            await using (Person personAsync = new("Timmy_Async")) ;
        }

        public static void Main()
        {
            using (Person person = new("usingPerson")) ;
            RunTimmy();
            GetHello();
            GC.Collect();
            Console.ReadLine();
        }
    }

    class Person : IDisposable, IAsyncDisposable
    {
        private readonly string _name;

        public Person(string name)
        {
            _name = name;
            Console.WriteLine($"Person {_name} was created");
        }

        ~Person()
        {
            Console.WriteLine($"Person {_name} was distructed");
        }

        public void Dispose()
        {
            Console.WriteLine($"Person {_name} was disposed");
        }
        
        public ValueTask DisposeAsync()
        {
            Console.WriteLine($"Person {_name} was disposed asyncly");
            return ValueTask.CompletedTask;
        }

        public void SayHello()
        {
            Console.WriteLine($"{_name}: Hello.");
        }
    }
}
