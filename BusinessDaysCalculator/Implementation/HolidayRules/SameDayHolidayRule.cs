using BusinessDaysCalculator.Interface;

namespace BusinessDaysCalculator.Implementation.HolidayRules;

/// <summary>
/// Rule Class for determining a fixed day holiday.
/// </summary>
public class SameDayHolidayRule : IHolidayRule
{
    private readonly int _holidayDay;
    private readonly int _holidayMonth;

    /// <summary>
    /// Constructor
    /// For e.g. If Anzac Day is on April 25th every year then.
    /// holidayMonth = 4, holidayDay = 25, holidayDayOfWeek = DayOfWeek.Monday
    /// </summary>
    /// <param name="holidayMonth"></param>
    /// <param name="holidayDay"></param>
    public SameDayHolidayRule(int holidayMonth, int holidayDay)
    {
        _holidayMonth = holidayMonth;
        _holidayDay = holidayDay;
    }

    /// <summary>
    /// Check if a valid the holiday date and the given date match for month and day.
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public bool IsValidHoliday(DateTime date)
    {
        return date.Month == _holidayMonth && date.Day == _holidayDay;
    }
}