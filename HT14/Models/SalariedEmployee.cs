using static testRepo.Programm; 

namespace HT14.Models
{
    public class SalariedEmployee : Employee
    {
        private int _workWeeksAmm = 0;
        private int _salaryPerWeek = 0;

        public override int Salary => _workWeeksAmm * _salaryPerWeek;

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

        public new virtual void Fill(Employee? employee)
        {
            SalaryPerWeek = PrintMessageAndGetValueInRange("Input worker's weekly pay", 0, int.MaxValue);
            WorkWeeksAmm = PrintMessageAndGetValueInRange("Input employee's worked weeks", 0, int.MaxValue);
            base.Fill(employee);
        }

        public override string ToString()=> base.ToString() + $", Weekly pay: {SalaryPerWeek}, Worked weeks: {WorkWeeksAmm}";
    }
}
