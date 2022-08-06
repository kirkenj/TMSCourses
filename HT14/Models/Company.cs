using static testRepo.Programm;

namespace HT14.Models
{
    public class Company
    {
        private static readonly Posts[] posts = Enum.GetValues(typeof(Posts)).Cast<Posts>().ToArray();
        private readonly List<Employee> _employees = new ()
        {
            new SalariedEmployee() { Name = "Nick", SalaryPerWeek = 900, WorkWeeksAmm = 30 }
        };

        public Company()
        {

        }

        public void ShowEmployeeEditMenu()
        {
            var emp = SeletctItemFromArray("Select employee", _employees.ToArray());
            if (emp == null)
            {
                Console.WriteLine("Employee hasn't been chosen");
                return;
            }

            emp.Fill();
        }

        public void ShowFireMenu()
        {
            var emp = SeletctItemFromArray("Select employee", _employees.ToArray());
            if (emp == null)
            {
                Console.WriteLine("Employee hasn't been chosen");
                return;
            }

            _employees.Remove(emp);
        }

        public void ShowAddEmployeeMenu()
        {
            var newPost = SeletctItemFromArray("Select new post", posts);
            if (newPost == default)
            {
                Console.WriteLine("New post hasn't been chosen");
                return;
            }

            Employee newEmployee = newPost switch
            {
                Posts.HourlyEmployee => new HourlyEmployee(),
                Posts.SalariedEmpployee => new SalariedEmployee(),
                Posts.Manager => new Manager(),
                Posts.Executive => new Executive(),
                _ => throw new ArgumentException("Invalid selection", nameof(newPost)),
            };
            newEmployee.Fill();
            _employees.Add(newEmployee);
        }

        public void ShowSetPostMenu()
        {
            var emp = SeletctItemFromArray("Select employee", _employees.ToArray());
            if (emp == null)
            {
                Console.WriteLine("Employee hasn't been chosen");
                return;
            }
            
            Console.WriteLine($"Selected employee: {emp}");
            var newPost = SeletctItemFromArray("Select new post", posts);
            if (newPost == default)
            {
                Console.WriteLine("New post hasn't been chosen");
                return;
            }

            Console.WriteLine($"Selected post: {newPost}");
            Employee modificatedEmployee = newPost switch
            {
                Posts.HourlyEmployee => new HourlyEmployee(),
                Posts.SalariedEmpployee => new SalariedEmployee(),
                Posts.Manager => new Manager(),
                Posts.Executive => new Executive(),
                _ => throw new ArgumentException("Invalid selection", nameof(newPost)),
            };
            modificatedEmployee.CopyFromEmployeeAndFillGaps(emp);
            _employees.Remove(emp);
            _employees.Add(modificatedEmployee);
        }

        public void PrintEmployees() => Console.WriteLine(string.Join("\n", _employees));
    }
}
