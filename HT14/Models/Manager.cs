using static testRepo.Programm;

namespace HT14.Models
{
    public class Manager : SalariedEmployee
    {
        private int _totalProjectsTurnover;
        private int _turnoverPercentForManager;

        public int TotalProjectsTurnover
        {
            get => _totalProjectsTurnover;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _totalProjectsTurnover = value;
            }
        }

        public int ManagersFee
        {
            get => _turnoverPercentForManager;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                _turnoverPercentForManager = value;
            }
        }
        
        public override Posts Post => Posts.Manager;
        public override void Fill() => Fill(this);
        public override int Salary => base.Salary + _totalProjectsTurnover * _turnoverPercentForManager / 100;
        public override string ToString() => base.ToString() + $", Total projects turnover {_totalProjectsTurnover}, Manager's pay percent: {_turnoverPercentForManager}";
        public override void CopyFromEmployeeAndFeelGaps(Employee employee) => CopyAndFillGaps(employee, this);

        public static void Fill(Manager manager) 
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            SalariedEmployee.Fill(manager);
            manager.ManagersFee = PrintMessageAndGetValueInRange("Input manager's fee percent", 0, 100);
            manager.TotalProjectsTurnover = PrintMessageAndGetValueInRange("Input manager's projects turnover", 0, int.MaxValue);
        }

        protected static void CopyAndFillGaps(Employee source, Manager destination)
        {
            SalariedEmployee.CopyAndFillGaps(source, destination);
            if (source is Manager managerSource)
            {
                destination.TotalProjectsTurnover = managerSource.TotalProjectsTurnover;
                destination.ManagersFee = managerSource.ManagersFee;
            }
            else
            {
                Console.WriteLine($"Input values for: {destination}");
                destination.ManagersFee = PrintMessageAndGetValueInRange("Input manager's fee percent", 0, 100);
                destination.TotalProjectsTurnover = PrintMessageAndGetValueInRange("Input manager's projects turnover", 0, int.MaxValue);
            }
        } 
    }
}
