using static testRepo.Programm;

namespace HT14.Models
{
    public class HourlyEmployee : Employee
    {
        private int _workHoursAmm = 0;
        private int _salaryPerHour = 0;

        public override int Salary 
        { 
            get => _workHoursAmm * _salaryPerHour;
        }

        public int WorkHoursAmm
        {
            get => _workHoursAmm;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _workHoursAmm = value;
            }
        }

        public int SalaryPerHour
        {
            get => _salaryPerHour;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _salaryPerHour = value;
            }
        }

        public override Posts Post => Posts.HourlyEmployee;
        public override void Fill() => Fill(this);
        public override string ToString() => base.ToString() + $", Hours: {_workHoursAmm}, Salary per hour: {_salaryPerHour}";
        public override void CopyFrom(Employee employee) => CopyAndFillGaps(employee, this);

        public static void Fill(HourlyEmployee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            Employee.Fill(employee);
            employee.SalaryPerHour = PrintMessageAndGetValueInRange("Input hourly worker's salary per hour", 0, int.MaxValue);
            employee.WorkHoursAmm = PrintMessageAndGetValueInRange("Input hourly worker's current work hours", 0, int.MaxValue);
        }

        protected static void CopyAndFillGaps(Employee source, HourlyEmployee destination)
        {
            if (source is not HourlyEmployee houredSource)
            {
                Copy(source, destination);
                destination.SalaryPerHour = PrintMessageAndGetValueInRange("Input hourly worker's salary per hour", 0, int.MaxValue);
                destination.WorkHoursAmm = PrintMessageAndGetValueInRange("Input hourly worker's current work hours", 0, int.MaxValue);
            }
            else
            {
                destination.WorkHoursAmm = houredSource.WorkHoursAmm;
                destination.SalaryPerHour = houredSource.SalaryPerHour;
            }
        }
    }
}
