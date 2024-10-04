namespace BusinessDaysCalculator.Interface;

/// <summary>
/// Interface for complex rule based business day calculation methods.
/// </summary>
public interface IRuleBasedBusinessDayFunctions
{
    /// <summary>
    /// Calculates Business days between a date range after accounting for the given Holidays rules.
    /// </summary>
    /// <param name="firstDate"></param>
    /// <param name="secondDate"></param>
    /// <param name="holidayRules"></param>
    /// <returns></returns>
    public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<IHolidayRule> holidayRules);
}