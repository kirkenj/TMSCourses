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

        public int TurnoverPartForManager
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

        public new void Fill(Employee? employee)
        {
            base.Fill(employee);
            TurnoverPartForManager = PrintMessageAndGetValueInRange("Input manager's fee percent", 0, int.MaxValue);
            TotalProjectsTurnover = PrintMessageAndGetValueInRange("Input manager's projects turnover", 0, int.MaxValue);
        }

        public override int Salary => base.Salary + _totalProjectsTurnover * _turnoverPercentForManager / 100;
        public override string ToString() => base.ToString() + $", Total projects turnover {_totalProjectsTurnover}, Manager's pay percent: {_turnoverPercentForManager}";
    }
}
