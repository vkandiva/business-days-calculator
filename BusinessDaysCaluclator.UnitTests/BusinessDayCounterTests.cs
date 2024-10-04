using BusinessDaysCalculator.Implementation;
using BusinessDaysCalculator.Implementation.HolidayRules;
using BusinessDaysCalculator.Interface;

namespace BusinessDaysCalculator.IntegationTests;

public class BusinessDayCounterTests
{
    private readonly IBusinessDayFunctions _businessDayFunctions = new BusinessDayCounter();
    private readonly IRuleBasedBusinessDayFunctions _ruleBasedBusinessDayFunctions = new BusinessDayCounter();

    private readonly List<IHolidayRule> _holidayRules = new()
                                                        {
                                                            new SameDayHolidayRule(12, 25), // Christmas
                                                            new SameDayHolidayRule(12, 26), // Boxing Day
                                                            new NextDayHolidayRule(1, 1), // New Year's Day
                                                            new OccurrenceBasedHolidayRule(6, 2, DayOfWeek.Monday) // Queens Birthday
                                                        };

    [TestCase("2013-10-07", "2013-10-09", 1)]
    [TestCase("2013-10-05", "2013-10-14", 5)]
    [TestCase("2013-10-07", "2014-01-01", 61)]
    [TestCase("2013-10-07", "2013-10-07", 0)] // Start = End date
    [TestCase("2013-10-07", "2013-10-05", 0)] // End date > start date
    [TestCase("2024-02-28", "2024-03-01", 1)] // Leap year (2024)
    [TestCase("2023-02-28", "2023-03-01", 0)] // Non-leap year
    [TestCase("2023-12-29", "2024-01-02", 1)] // End of year weekend
    [TestCase("2024-04-01", "2024-04-05", 3)]
    [TestCase("2023-07-01", "2023-07-10", 5)] // Multiple weekends
    [TestCase("2023-06-30", "2023-07-10", 5)]
    public void WeekdaysBetweenTwoDates_ShouldReturnCorrectResult(string startDate, string endDate, int expected)
    {
        // Arrange
        var firstDate = DateTime.Parse(startDate);
        var secondDate = DateTime.Parse(endDate);

        // Act
        var result = _businessDayFunctions.WeekdaysBetweenTwoDates(firstDate, secondDate);

        // Assert
        Assert.AreEqual(expected, result);
    }


    [TestCase("2013-10-07", "2013-10-09", 1, new[] { "2013-12-25", "2013-12-26", "2014-01-01" })]
    [TestCase("2013-12-24", "2013-12-27", 0, new[] { "2013-12-25", "2013-12-26", "2014-01-01" })]
    [TestCase("2013-10-07", "2014-01-01", 59, new[] { "2013-12-25", "2013-12-26", "2014-01-01" })]
    [TestCase("2023-12-23", "2023-12-27", 0, new[] { "2023-12-25", "2023-12-26" })] // All holidays
    [TestCase("2013-10-07", "2013-10-05", 0, new string[] { })] // End date > start date
    [TestCase("2024-05-04", "2024-05-04", 0, new string[] { })] // Same day
    [TestCase("2024-09-23", "2024-09-27", 3, new string[] { })]
    [TestCase("2024-09-23", "2024-09-27", 3, new[] { "2024-09-23" })] // First day as holiday?
    [TestCase("2024-09-23", "2024-09-27", 3, new[] { "2024-09-27" })] // Last day as holiday?
    [TestCase("2024-10-02", "2024-10-10", 5, new[] { "2024-10-05" })] // Saturday as holiday?
    [TestCase("2024-10-02", "2024-10-10", 5, new[] { "2024-10-05", "2024-10-06" })] // Saturday and Sunday as holiday?
    public void BusinessDaysBetweenTwoDates_ShouldReturnCorrectResult(string startDate, string endDate, int expected,
        string[] holidays)
    {
        // Arrange
        var firstDate = DateTime.Parse(startDate);
        var secondDate = DateTime.Parse(endDate);
        var publicHolidays = holidays.Select(DateTime.Parse).ToList();

        // Act
        var result = _businessDayFunctions.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);

        // Assert
        Assert.AreEqual(expected, result);
    }


    [TestCase("2013-10-07", "2014-01-01", 59)]
    [TestCase("2022-12-28", "2023-01-06", 5)] // Moveable business days
    [TestCase("2022-12-30", "2023-01-03", 0)] // No business days case
    [TestCase("2024-06-06", "2024-06-11", 1)] // Test Queens birthday case
    public void BusinessDaysBetweenTwoDates_WithHolidayRules_ShouldReturnCorrectResult(string startDate, string endDate, int expected)
    {
        // Arrange
        var firstDate = DateTime.Parse(startDate);
        var secondDate = DateTime.Parse(endDate); // Wednesday

        // Act
        var result = _ruleBasedBusinessDayFunctions.BusinessDaysBetweenTwoDates(firstDate, secondDate, _holidayRules);

        // Assert
        Assert.AreEqual(expected, result);
    }
}