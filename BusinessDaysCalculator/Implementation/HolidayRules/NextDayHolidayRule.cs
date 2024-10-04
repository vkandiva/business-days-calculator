using BusinessDaysCalculator.Interface;

namespace BusinessDaysCalculator.Implementation.HolidayRules;

/// <summary>
/// Rule class to determine if a holiday falls on a day immediately following a weekend holiday.
/// </summary>
public class NextDayHolidayRule : IHolidayRule
{
    private readonly int _holidayDay;
    private readonly int _holidayMonth;

    /// <summary>
    /// Constructor
    /// For e.g. If New Years is on 1st of Jan every year 
    /// holidayMonth = 1, holidayDay = 1
    /// </summary>
    /// <param name="holidayMonth"></param>
    /// <param name="holidayDay"></param>
    public NextDayHolidayRule(int holidayMonth, int holidayDay)
    {
        _holidayMonth = holidayMonth;
        _holidayDay = holidayDay;
    }

    /// <summary>
    /// If the holiday date is a weekend move it to the next business day and compare.
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public bool IsValidHoliday(DateTime date)
    {
        var newHolidayDate = new DateTime(date.Year, _holidayMonth, _holidayDay);

        // If it falls on a weekend, the holiday is moved to the next Monday
        if (newHolidayDate.DayOfWeek == DayOfWeek.Saturday)
        {
            newHolidayDate = newHolidayDate.AddDays(2);
        }
        else if (newHolidayDate.DayOfWeek == DayOfWeek.Sunday)
        {
            newHolidayDate = newHolidayDate.AddDays(1);
        }

        return newHolidayDate.Date == date.Date;
    }
}