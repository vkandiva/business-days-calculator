using BusinessDaysCalculator.Implementation.HolidayRules;

namespace BusinessDaysCalculator.UnitTests;

[TestFixture]
public class OccurrenceBasedHolidayRuleTests
{
    // Valid Tests
    [TestCase(6, 2, DayOfWeek.Monday, "2024-06-10", ExpectedResult = true, TestName = "Queens_Birthday_Valid_Test")]
    [TestCase(8, 1, DayOfWeek.Thursday, "2024-08-01", ExpectedResult = true, TestName = "First_DayInMonth_Valid_Test")]
    [TestCase(8, 2, DayOfWeek.Thursday, "2024-08-08", ExpectedResult = true, TestName = "SecondOccurence_FirstDayInMonth_Valid_Test")]
    [TestCase(10, 5, DayOfWeek.Thursday, "2024-10-31", ExpectedResult = true, TestName = "Last_DayInMonth_Valid_Test")]
    [TestCase(2, 4, DayOfWeek.Tuesday, "2024-02-27", ExpectedResult = true, TestName = "FourthTuesdayInFebruary_Valid")]
    [TestCase(2, 5, DayOfWeek.Thursday, "2024-02-29", ExpectedResult = true, TestName = "LeapYear_LastDay_Valid_Test")]
    [TestCase(2, 4, DayOfWeek.Tuesday, "2023-02-28", ExpectedResult = true, TestName = "NonLeapYear_LastDay_Valid_Test")]
    // Invalid Tests
    [TestCase(2, 5, DayOfWeek.Wednesday, "2023-02-28", ExpectedResult = false, TestName = "Greater_Occurence_Range_Invalid_Test")]
    [TestCase(10, 2, DayOfWeek.Monday, "2024-10-04", ExpectedResult = false, TestName = "Incorrect_Occurence_Invalid_Test")]
    [TestCase(9, 2, DayOfWeek.Monday, "2024-10-14", ExpectedResult = false, TestName = "Incorrect_Month_Invalid_Test")]
    public bool IsValidHoliday_TestCases(int month, int holidayOccurrence, DayOfWeek holidayDayOfWeek, string dateString)
    {
        // Arrange
        var holidayRule = new OccurrenceBasedHolidayRule(month, holidayOccurrence, holidayDayOfWeek);
        var testDate = DateTime.Parse(dateString);

        // Act
        return holidayRule.IsValidHoliday(testDate);
    }
}