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

        public override void Fill(Employee? employee)
        {
            SalaryPerHour = PrintMessageAndGetValueInRange("Input hourly worker's salary per hour", 0, int.MaxValue);
            WorkHoursAmm = PrintMessageAndGetValueInRange("Input hourly worker's current work hours", 0, int.MaxValue);
            base.Fill(employee);
        }

        public override string ToString() => base.ToString() + $", Salary type: Hourly employee, Hours: {_workHoursAmm}, Salary per hour: {_salaryPerHour}";
    }
}
