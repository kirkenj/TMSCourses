namespace HT14.Models
{
    public abstract class Employee
    {
        public abstract int Salary { get; }
        public string Name { get; set; } = string.Empty;
        public virtual void Fill()
        {
            Console.Write("Input worker's name:");
            var buffName = Console.ReadLine();
            Name = string.IsNullOrEmpty(buffName) ? "Invalid name" : buffName;
        }
        public virtual void Fill(Employee? employee = null)
        {
            if (employee == null)
            {
                Console.Write("Input worker's name:");
                var buffName = Console.ReadLine();
                Name = string.IsNullOrEmpty(buffName) ? "Invalid name" : buffName.Trim();
            }
            else if (employee == this)
            {
                throw new ArgumentException("U trying to fill an employee by himself", nameof(employee));
            }
            else
            {
                Name = employee.Name ?? "Invalid name";
            }
        }

        public override string ToString() => $"Name: {Name}, Salary: {Salary}";
    }
}
