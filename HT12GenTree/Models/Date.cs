namespace HT12GenTree.Models
{
    public class Date
    {
        private int _fullDaysAmm = 0;
        private const int DAYS_AMM_NOT_LEAP_YEAR = 365;
        private const int DAYS_AMM_LEAP_YEAR = DAYS_AMM_NOT_LEAP_YEAR + 1;
        private const int DAYS_AMM_400_YEARS_SINCE_DATE_START = 146097;


        public static int GetDaysAmmInMonthByNumber(int monthNumber, bool isLeapYear)
        {
            return monthNumber switch
            {
                1 => 31,
                2 => isLeapYear ? 29 : 28,
                3 => 31,
                4 => 30,
                5 => 31,
                6 => 30,
                7 => 31,
                8 => 31,
                9 => 30,
                10 => 31,
                11 => 30,
                12 => 31,
                _ => throw new ArgumentOutOfRangeException(nameof(monthNumber)),
            };
        }

        public static int GetDaysAmmInPrevYears(int yearNumber)
        {
            if (yearNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(yearNumber));
            }

            var yearsAmm = yearNumber - 1;

            int ret = yearsAmm / DAYS_AMM_400_YEARS_SINCE_DATE_START;
            yearsAmm %= DAYS_AMM_400_YEARS_SINCE_DATE_START;
            for (int i = 1; i <= yearsAmm; i++)
            {
                ret += IsLeapYear(i) ? DAYS_AMM_LEAP_YEAR : DAYS_AMM_NOT_LEAP_YEAR;
            }

            return ret;
        }

        public static bool IsLeapYear(int year)
        {
            if (year <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(year));
            }

            return (year % 100 == 0 && year % 400 == 0) || (year % 100 != 0 && year % 4 == 0);
        }
        public Date(int days, int month, int years)
        {
            if (years <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(years));
            }


        }
    }
}
