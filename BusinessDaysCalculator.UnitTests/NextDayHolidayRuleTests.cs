using BusinessDaysCalculator.Implementation.HolidayRules;

namespace BusinessDaysCalculator.UnitTests;

public class NextDayHolidayRuleTests
{
    // Week day holidays; holidays won't be moved
    [TestCase(2024, 5, 1, 5, 1, true, TestName = "Holiday_On_Weekday_May1_ValidHoliday")]
    [TestCase(2023, 12, 25, 12, 25, true, TestName = "Holiday_On_Weekday_Christmas_ValidHoliday")]
    // If holiday falls on Saturday.
    [TestCase(2024, 5, 6, 5, 4, true, TestName = "Holiday_On_Saturday_ValidHoliday")]
    [TestCase(2022, 1, 3, 1, 1, true, TestName = "NewYear_Holiday_On_Saturday_ValidHoliday")]
    // If Holiday falls on a Sunday.
    [TestCase(2024, 5, 6, 5, 5, true, TestName = "Holiday_On_Sunday_ValidHoliday")]
    [TestCase(2023, 12, 25, 12, 24, true, TestName = "BoxingDay_On_Sunday_ValidHoliday")]
    // Leap year case
    [TestCase(2024, 2, 29, 2, 29, true, TestName = "LeapYearHoliday_ValidHoliday")]
    [TestCase(2023, 2, 28, 2, 29, false, TestName = "NonLeapYear_InValidHoliday")]
    // Non-holiday day
    [TestCase(2024, 5, 2, 5, 1, false, TestName = "NonHolidayDay_Invalid_Test")]
    [TestCase(2024, 12, 27, 12, 25, false, TestName = "NonChristmasDay_Invalid_Test")]
    public void IsValidHoliday_WeekdayHoliday_ShouldReturnCorrectResult(int year, int testMonth, int testDay,
        int holidayMonth, int holidayDay, bool expected)
    {
        // Arrange
        var holidayRule = new NextDayHolidayRule(holidayMonth, holidayDay);

        try
        {
            // Act
            var isValid = holidayRule.IsValidHoliday(new DateTime(year, testMonth, testDay));

            // Assert
            Assert.AreEqual(expected, isValid);
        }
        catch (Exception ex)
        {
            Assert.False(false, "Test threw an exception" + ex.Message);
        }
    }
}