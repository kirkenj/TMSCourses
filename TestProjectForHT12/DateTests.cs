using NUnit.Framework;
using System;
using HT12GenTree.Models;
using Assert = NUnit.Framework.Assert;

namespace TestProjectForHT12
{
    public class DateTests
    {
        [TestCase(4)]
        [TestCase(8)]
        [TestCase(12)]
        [TestCase(16)]
        [TestCase(400)]
        [TestCase(800)]
        [TestCase(1600)]
        [TestCase(2000)]
        public void DateIsLeapYearTests_ReturnsTrue(int yearNumber)
        {
            Assert.True(Date.IsLeapYear(yearNumber));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(200)]
        [TestCase(1300)]
        [TestCase(900)]
        public void DateIsLeapYearTests_ReturnsFalse(int yearNumber)
        {
            Assert.False(Date.IsLeapYear(yearNumber));
        }
        
        [TestCase(0)]
        [TestCase(-1)]
        public void DateIsLeapYearTests_ThrowsArgumentOutOFRangeException(int yearNumber)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Date.IsLeapYear(yearNumber));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void DateGetDaysAmmInPrevYears_ThrowsArgumentOutOFRangeException(int yearNumber)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Date.GetDaysAmmInPrevYears(yearNumber));
        }

        [TestCase(1, ExpectedResult = 0)]
        [TestCase(2, ExpectedResult = 365)]
        [TestCase(3, ExpectedResult = 730)]
        [TestCase(4, ExpectedResult = 1095)]
        [TestCase(5, ExpectedResult = 1461)]
        [TestCase(31, ExpectedResult = 10957)]
        [TestCase(101, ExpectedResult = 36524)]
        [TestCase(131, ExpectedResult = 47481)]
        [TestCase(401, ExpectedResult = 146097)]
        [TestCase(1201, ExpectedResult = 438291)]
        [TestCase(1231, ExpectedResult = 449248)]
        [TestCase(1331, ExpectedResult = 485772)]
        public int DateGetDaysAmmInPrevYears_Returnsvalue(int yearNumber) => Date.GetDaysAmmInPrevYears(yearNumber);

        [TestCase(1, false, ExpectedResult = 0)]
        [TestCase(2, false, ExpectedResult = 31)]
        [TestCase(3, false, ExpectedResult = 59)]
        [TestCase(4, false, ExpectedResult = 90)]
        [TestCase(5, false, ExpectedResult = 120)]
        [TestCase(6, false, ExpectedResult = 151)]
        [TestCase(7, false, ExpectedResult = 181)]
        [TestCase(8, false, ExpectedResult = 212)]
        [TestCase(9, false, ExpectedResult = 243)]
        [TestCase(10, false, ExpectedResult = 273)]
        [TestCase(11, false, ExpectedResult = 304)]
        [TestCase(12, false, ExpectedResult = 334)]
        [TestCase(3, true, ExpectedResult = 60)]
        [TestCase(4, true, ExpectedResult = 91)]
        [TestCase(5, true, ExpectedResult = 121)]
        [TestCase(6, true, ExpectedResult = 152)]
        [TestCase(7, true, ExpectedResult = 182)]
        [TestCase(8, true, ExpectedResult = 213)]
        [TestCase(9, true, ExpectedResult = 244)]
        [TestCase(10, true, ExpectedResult = 274)]
        [TestCase(11, true, ExpectedResult = 305)]
        [TestCase(12, true, ExpectedResult = 335)]        
        public int DateGetDaysAmmInPrevMonth_Returnsvalue(int monthNumber, bool isLeap) => Date.GetDaysAmmInPrevMonth(monthNumber, isLeap);
 
        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(3, false)]
        [TestCase(4, false)]
        [TestCase(5, false)]
        [TestCase(6, false)]
        [TestCase(7, false)]
        [TestCase(8, false)]
        [TestCase(9, false)]
        [TestCase(10, false)]
        [TestCase(11, false)]
        [TestCase(12, false)]
        [TestCase(3, true)]
        [TestCase(4, true)]
        [TestCase(5, true)]
        [TestCase(6, true)]
        [TestCase(7, true)]
        [TestCase(8, true)]
        [TestCase(9, true)]
        [TestCase(10, true)]
        [TestCase(11, true)]
        [TestCase(12, true)]        
        public void DateGetMonthAmm_TestVia_DateGetDaysAmmInPrevMonth(int monthNumber, bool isLeap)
        {
            var days = Date.GetDaysAmmInPrevMonth(monthNumber, isLeap) + 15;
            Assert.AreEqual(monthNumber, Date.GetMonthNumberByPassedDays(days, isLeap));
        }


        [TestCase(1, ExpectedResult = 1)]
        [TestCase(365, ExpectedResult = 2)]
        [TestCase(730, ExpectedResult = 3)]
        [TestCase(1095, ExpectedResult = 4)]
        [TestCase(1461, ExpectedResult = 5)]
        [TestCase(10957, ExpectedResult = 31)]
        [TestCase(36524, ExpectedResult = 101)]
        [TestCase(47481, ExpectedResult = 131)]
        [TestCase(146097, ExpectedResult = 401)]
        [TestCase(146096, ExpectedResult = 400)]
        [TestCase(438291, ExpectedResult = 1201)]
        [TestCase(449248, ExpectedResult = 1231)]
        [TestCase(485772, ExpectedResult = 1331)]
        [TestCase(1460, ExpectedResult = 4)]
        public int DateGetCurrentYearByPassedDaysAmm_Returnsvalue(int dayAmm) => Date.GetCurrentYearNumberByPassedDaysAmm(dayAmm);

        [TestCase(ExpectedResult = true)]
        public bool MegaTest()
        {
            DateTime date = new (1,1,1);
            Date dateUser = new (1,1,1);
            for (int i = 0; i < (date - DateTime.MaxValue).Days; i++)
            {
                if (!(date.Year == dateUser.Year && date.Month == dateUser.Month && date.Day == dateUser.Day))
                {
                    return false;
                }
                date = date.AddDays(1);
                dateUser.AddDays(1);
            }
            return true;
        }
    }
}