using static testRepo.Programm; 

namespace HT14.Models
{
    public class SalariedEmployee : Employee
    {
        private int _workWeeksAmm = 0;
        private int _salaryPerWeek = 0;
        public override int Salary => _workWeeksAmm * _salaryPerWeek;
        public override Posts Post => Posts.SalariedEmpployee;
        public int WorkWeeksAmm
        {
            get => _workWeeksAmm;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _workWeeksAmm = value;
            }
        }
        public int SalaryPerWeek
        {
            get => _salaryPerWeek;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _salaryPerWeek = value;
            }
        }
        public override void Fill() => Fill(this);
        public override string ToString()=> base.ToString() + $", Weekly pay: {SalaryPerWeek}, Worked weeks: {WorkWeeksAmm}";
        public override void CopyFromEmployeeAndFillGaps(Employee employee) => CopyAndFillGaps(employee, this);
        public static void Fill(SalariedEmployee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            Employee.Fill(employee);
            employee.SalaryPerWeek = PrintMessageAndGetValueInRange("Input employee's weekly pay", 0, int.MaxValue);
            employee.WorkWeeksAmm = PrintMessageAndGetValueInRange("Input employee's worked weeks", 0, int.MaxValue);
        }

        protected static void CopyAndFillGaps(Employee source, SalariedEmployee destination)
        {
            Copy(source, destination);
            if (source is SalariedEmployee salariedSource)
            {
                destination.SalaryPerWeek = salariedSource.SalaryPerWeek;
                destination.WorkWeeksAmm = salariedSource.WorkWeeksAmm;
            }
            else
            {
                Console.WriteLine($"Input values for: {destination}");
                destination.SalaryPerWeek = PrintMessageAndGetValueInRange("Input employee's weekly pay", 0, int.MaxValue);
                destination.WorkWeeksAmm = PrintMessageAndGetValueInRange("Input employee's worked weeks", 0, int.MaxValue);
            }
        }
    }
}
