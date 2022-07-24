﻿namespace HT12GenTree.Models
{
    public class Date
    {
        private int _fullDaysAmm = 0;
        private int _yearNumber;
        private int _monthNumber;
        private int _dayNumber;

        private const int DAYS_AMM_NOT_LEAP_YEAR = 365;
        private const int DAYS_AMM_LEAP_YEAR = DAYS_AMM_NOT_LEAP_YEAR + 1;
        private const int DAYS_AMM_4_YEARS_SINCE_DATE_START = DAYS_AMM_NOT_LEAP_YEAR * 4 + 1;
        private const int DAYS_AMM_100_YEARS_SINCE_DATE_START = 36524;
        private const int DAYS_AMM_400_YEARS_SINCE_DATE_START = 146097;

        public Date(int dayNumber, int monthNumber, int yearNumber)
        {
            if (yearNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(yearNumber));
            }

            _fullDaysAmm = GetDaysAmmInPrevYears(yearNumber);
            _yearNumber = yearNumber;
            bool isLeapYear = IsLeapYear(yearNumber);

            if (monthNumber < 1 || monthNumber > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(monthNumber));
            }

            _fullDaysAmm += GetDaysAmmInPrevMonth(monthNumber, isLeapYear);
            _monthNumber = monthNumber;
        
            if (dayNumber < 1 || dayNumber > GetDaysAmmInMonthByNumber(monthNumber, isLeapYear)) 
            {
                throw new ArgumentOutOfRangeException(nameof(dayNumber));
            }

            _fullDaysAmm += dayNumber - 1;
            _dayNumber = dayNumber;
        }

        private void UpdateAll()
        {
            var days = _fullDaysAmm;
            _yearNumber = GetCurrentYearNumberByPassedDaysAmm(days);
            var viaYears = GetDaysAmmInPrevYears(_yearNumber);
            days -= viaYears;
            _monthNumber = GetMonthNumberByPassedDays(days, IsLeapYear(_yearNumber));
            days -= GetDaysAmmInPrevMonth(_monthNumber, IsLeapYear(_yearNumber));
            _dayNumber = days + 1;
        }

        public int Year 
        { 
            get => _yearNumber;
        }

        public int Month
        {
            get => _monthNumber;
        }

        public int Day
        {
            get => _dayNumber;
        }

        public bool IsLeap => IsLeapYear(Year);

        public void AddDays(int days)
        {
            if (_fullDaysAmm + days < 0)
            {
                throw new ArgumentException("That addition will make the object invalid", nameof(days));
            }

            _fullDaysAmm += days;
            UpdateAll();
        }

        public static int GetDaysAmmInMonthByNumber(int monthNumber, bool isLeapYear) => monthNumber switch
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

        public static int GetDaysAmmInPrevMonth(int monthNumber, bool isLeapYear) => monthNumber switch
        {
            1 => 0,
            2 => 31,
            3 => 59,
            4 => 90,
            5 => 120,
            6 => 151,
            7 => 181,
            8 => 212,
            9 => 243,
            10 => 273,
            11 => 304,
            12 => 334,
            _ => throw new ArgumentOutOfRangeException(nameof(monthNumber))
        } + (monthNumber > 2 && isLeapYear ? 1 : 0);

        public static int GetMonthNumberByPassedDays(int daysAmm, bool isLeapYear)
        {
            if (daysAmm < 0 || (isLeapYear && daysAmm >= 366) || (!isLeapYear && daysAmm >= 365))
            {
                throw new ArgumentOutOfRangeException(nameof(daysAmm));
            }

            if (daysAmm <= 30)
            {
                return 1;
            }

            if (daysAmm <= 58 + (isLeapYear ? 1 : 0))
            {
                return 2;
            }

            if (daysAmm <= 89 + (isLeapYear ? 1 : 0))
            {
                return 3;
            }

            if (daysAmm <= 119 + (isLeapYear ? 1 : 0))
            {
                return 4;
            }

            if (daysAmm <= 150 + (isLeapYear ? 1 : 0))
            {
                return 5;
            }
            
            if (daysAmm <= 180 + (isLeapYear ? 1 : 0))
            {
                return 6;
            }
            
            if (daysAmm <= 211 + (isLeapYear ? 1 : 0))
            {
                return 7;
            }
            
            if (daysAmm <= 242 + (isLeapYear ? 1 : 0))
            {
                return 8;
            }
            
            if (daysAmm <= 272 + (isLeapYear ? 1 : 0))
            {
                return 9;
            }
            
            if (daysAmm <= 303 + (isLeapYear ? 1 : 0))
            {
                return 10;
            }
            
            if (daysAmm <= 333 + (isLeapYear ? 1 : 0))
            {
                return 11;
            }

            return 12;
        }

        public static int GetDaysAmmInPrevYears(int yearNumber)
        {
            if (yearNumber <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(yearNumber));
            }

            var yearsAmm = yearNumber - 1;

            int ret = (yearsAmm / 400) * DAYS_AMM_400_YEARS_SINCE_DATE_START;
            yearsAmm %= 400;
            ret += (yearsAmm / 100) * DAYS_AMM_100_YEARS_SINCE_DATE_START;
            yearsAmm %= 100;
            ret += (yearsAmm / 4) * DAYS_AMM_4_YEARS_SINCE_DATE_START;
            yearsAmm %= 4;
            ret += yearsAmm * DAYS_AMM_NOT_LEAP_YEAR;
            return ret;
        }

        public static bool IsLeapYear(int year)
        {
            if (year <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(year));
            }

            return (year % 400 == 0) || (year % 100 != 0 && year % 4 == 0);
        }

        public static int GetCurrentYearNumberByPassedDaysAmm(int dayAmm)
        {
            if (dayAmm < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dayAmm));
            }

            int ret = 1;
            ret += (dayAmm / DAYS_AMM_400_YEARS_SINCE_DATE_START) * 400;
            dayAmm %= DAYS_AMM_400_YEARS_SINCE_DATE_START;
            var buffDev = dayAmm / DAYS_AMM_100_YEARS_SINCE_DATE_START;
            buffDev = buffDev == 4 ? 3 : buffDev;
            ret += (buffDev) * 100;
            dayAmm -= DAYS_AMM_100_YEARS_SINCE_DATE_START * buffDev;
            ret += (dayAmm / DAYS_AMM_4_YEARS_SINCE_DATE_START) * 4;
            dayAmm %= DAYS_AMM_4_YEARS_SINCE_DATE_START;
            buffDev = dayAmm / DAYS_AMM_NOT_LEAP_YEAR;
            ret += buffDev == 4 ? 3 : buffDev;
            return ret;
        }
    }
}
