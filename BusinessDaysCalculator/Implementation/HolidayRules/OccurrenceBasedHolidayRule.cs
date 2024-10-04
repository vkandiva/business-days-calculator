using BusinessDaysCalculator.Interface;
using System.Text;

namespace BusinessDaysCalculator.Implementation.HolidayRules;

/// <summary>
/// Rule class for determining an if a holiday occurs on a certain day of a month.
/// </summary>
public class OccurrenceBasedHolidayRule : IHolidayRule
{
    private readonly DayOfWeek _holidayDayOfWeek;
    private readonly int _holidayMonth;
    private readonly int _holidayOccurrence;

    /// <summary>
    /// Constructor
    /// For e.g. If Queen's Birthday is on the second Monday in June every year then
    /// holidayMonth = 6, holidayOccurrence = 2, holidayDayOfWeek = DayOfWeek.Monday
    /// </summary>
    /// <param name="holidayMonth"></param>
    /// <param name="holidayDayOfWeek"></param>
    /// <param name="holidayOccurrence"></param>
    public OccurrenceBasedHolidayRule(int holidayMonth, int holidayOccurrence, DayOfWeek holidayDayOfWeek)
    {
        if (holidayOccurrence < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(holidayOccurrence),
                "Holiday Occurrence must be greater than zero.");
        }

        if (holidayMonth < 1 || holidayMonth > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(holidayMonth), "Month must be between 1 and 12.");
        }

        _holidayMonth = holidayMonth;
        _holidayDayOfWeek = holidayDayOfWeek;
        _holidayOccurrence = holidayOccurrence;
    }

    /// <summary>
    /// Check date against the given occurence in the month with the given day for a particular year. 
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public bool IsValidHoliday(DateTime date)
    {
        if (date.Month != _holidayMonth || date.DayOfWeek != _holidayDayOfWeek)
        {
            return false;
        }

        // First day of the month in the year
        var firstDayOfMonth = new DateTime(date.Year, _holidayMonth, 1);
        // Get the first occurrence of the day in the month
        var firstHolidayDayOfMonth = firstDayOfMonth.AddDays((_holidayDayOfWeek - firstDayOfMonth.DayOfWeek + 7) % 7);
        // Calculate the exact date for the occurrence (e.g., 2nd Monday, 3rd Wednesday, etc.)
        var occurrenceBasedHolidayDate = firstHolidayDayOfMonth.AddDays((_holidayOccurrence - 1) * 7);

        return date.Date == occurrenceBasedHolidayDate.Date;
    }
}