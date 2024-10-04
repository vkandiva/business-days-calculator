using BusinessDaysCalculator.Implementation.HolidayRules;

namespace BusinessDaysCalculator.UnitTests;

[TestFixture]
public class SameDayHolidayRuleTests
{
    [TestCase(12, 25, "2024-12-25", ExpectedResult = true, TestName = "ExactHoliday_Valid_Test")]
    [TestCase(12, 25, "2024-12-24", ExpectedResult = false, TestName = "DayBeforeHoliday_InValid_Test")]
    [TestCase(12, 25, "2024-12-26", ExpectedResult = false, TestName = "DayAfterHoliday_InValid_Test")]
    [TestCase(12, 25, "2024-11-25", ExpectedResult = false, TestName = "WrongMonthHoliday_InValid_Test")]
    [TestCase(2, 29, "2024-02-29", ExpectedResult = true, TestName = "LeapYearHoliday_Valid_Test")]
    [TestCase(2, 29, "2023-02-28", ExpectedResult = false, TestName = "NonLeapYearHoliday_InValid_Test")]
    [TestCase(1, 1, "2024-01-01", ExpectedResult = true, TestName = "NewYearsDayHoliday_Valid_Test")]
    [TestCase(12, 31, "2024-12-31", ExpectedResult = true, TestName = "EndOfYearHoliday_Valid_Test")]
    [TestCase(7, 4, "2024-08-15", ExpectedResult = false, TestName = "RandomDateHoliday_InValid_Test")]
    public bool IsValidHoliday_GivenDate_ShouldReturnExpectedResult(int holidayMonth, int holidayDay, string testDate)
    {
        // Arrange
        var holidayRule = new SameDayHolidayRule(holidayMonth, holidayDay);
        var dateToTest = DateTime.Parse(testDate);

        // Act
        return holidayRule.IsValidHoliday(dateToTest);
    }
}