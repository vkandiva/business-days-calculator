namespace BusinessDaysCalculator.Interface;

/// <summary>
/// Interface for simple business day / weekday calculation methods.
/// </summary>
public interface IBusinessDayFunctions
{
    /// <summary>
    /// Calculates Weekdays between a date range
    /// </summary>
    /// <param name="firstDate"></param>
    /// <param name="secondDate"></param>
    /// <returns></returns>
    public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate);

    /// <summary>
    /// Calculates Business days between a date range after accounting for the given Public Holidays.
    /// </summary>
    /// <param name="firstDate"></param>
    /// <param name="secondDate"></param>
    /// <param name="publicHolidays"></param>
    /// <returns></returns>
    public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays);
}