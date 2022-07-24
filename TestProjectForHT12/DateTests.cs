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
        [TestCase(101, ExpectedResult = 36524)]
        [TestCase(401, ExpectedResult = 146097)]
        [TestCase(1201, ExpectedResult = 438291)]
        public int DateGetDaysAmmInPrevYears_Returnsvalue(int yearNumber) => Date.GetDaysAmmInPrevYears(yearNumber);
    }
}