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
        public override void CopyFromEmployeeAndFillGaps(Employee employee) => CopyAndFillGaps(employee, this);

        public static void Fill(Manager manager) 
        {
            SalariedEmployee.Fill(manager);
            manager.ManagersFee = ReadIntFromConsole("Input manager's fee percent", 0, 100);
            manager.TotalProjectsTurnover = ReadIntFromConsole("Input manager's projects turnover", 0, int.MaxValue);
        }

        protected static void CopyAndFillGaps(Employee source, Manager destination)
        {
            SalariedEmployee.CopyAndFillGaps(source, destination);
            (destination.TotalProjectsTurnover, destination.ManagersFee) = source is Manager managerSource ? 
                (managerSource.TotalProjectsTurnover, managerSource.ManagersFee) :
                (ReadIntFromConsole("Input manager's projects turnover", 0, int.MaxValue), ReadIntFromConsole("Input manager's fee percent", 0, 100));
        } 
    }
}
