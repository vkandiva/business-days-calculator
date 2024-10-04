using BusinessDaysCalculator.Interface;

namespace BusinessDaysCalculator.Implementation;

/// <summary>
/// Implementation for calculating business days in a date range with fixed holidays or a set of holiday rules.
/// </summary>
public class BusinessDayCounter : IBusinessDayFunctions, IRuleBasedBusinessDayFunctions
{
    /// <summary>
    /// Calculates Weekdays between a date range.
    /// </summary>
    /// <param name="firstDate"></param>
    /// <param name="secondDate"></param>
    /// <returns></returns>
    public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
    {
        // Return 0 if secondDate is equal to or less than firstDate
        if (secondDate <= firstDate)
        {
            return 0;
        }

        firstDate = firstDate.AddDays(1);
        secondDate = secondDate.AddDays(-1);

        var weekdays = 0;
        // Is the current date is a weekday
        for (var date = firstDate; date <= secondDate; date = date.AddDays(1))
        {
            // If day is not a weekend.
            if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
            {
                weekdays++;
            }
        }

        return weekdays;
    }

    /// <summary>
    /// Calculates Business days between a date range after accounting for the given Public Holidays.
    /// </summary>
    /// <param name="firstDate"></param>
    /// <param name="secondDate"></param>
    /// <param name="publicHolidays"></param>
    /// <returns></returns>
    public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
    {
        // Return 0 if secondDate is equal to or less than firstDate
        if (secondDate <= firstDate)
        {
            return 0;
        }

        firstDate = firstDate.AddDays(1);
        secondDate = secondDate.AddDays(-1);

        var businessDays = 0;
        // Calculate business days
        for (var date = firstDate; date <= secondDate; date = date.AddDays(1))
        {
            // If day is not a weekend and not a public holiday increment business days.
            if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday &&
                !publicHolidays.Contains(date.Date))
            {
                businessDays++;
            }
        }

        return businessDays;
    }

    /// <summary>
    /// Calculates Business days between a date range after accounting for the given Holidays rules.
    /// </summary>
    /// <param name="firstDate"></param>
    /// <param name="secondDate"></param>
    /// <param name="holidayRules"></param>
    /// <returns></returns>
    public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<IHolidayRule> holidayRules)
    {
        if (secondDate <= firstDate)
        {
            return 0;
        }

        firstDate = firstDate.AddDays(1);
        secondDate = secondDate.AddDays(-1);

        var businessDays = 0;
        // Calculate business days
        for (var date = firstDate; date <= secondDate; date = date.AddDays(1))
        {
            // If day is not a weekend and not a valid holiday increment business days.
            if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday &&
                !holidayRules.Any(rule => rule.IsValidHoliday(date)))
            {
                businessDays++;
            }
        }

        return businessDays;
    }
}