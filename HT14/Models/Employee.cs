namespace HT14.Models
{
    public abstract class Employee
    {
        public abstract Posts Post { get; }
        public abstract int Salary { get; }
        public string Name { get; set; } = string.Empty;
        public virtual void Fill() => Fill(this);
        public override string ToString() => $"Name: {Name}, Salary: {Salary}, Post: {Post}";

        public static void Fill(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            Console.Write("Input worker's name:");
            var buffName = Console.ReadLine();
            employee.Name = string.IsNullOrEmpty(buffName) ? string.Empty : buffName.Trim();
        }

        public virtual void CopyFrom(Employee employee) => Copy(employee, this);
        
        protected static void Copy(Employee source, Employee destination)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source == destination)
            {
                throw new ArgumentException("Destination is equal to source");
            }

            destination.Name = source.Name;
        }
    }
}
