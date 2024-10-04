namespace BusinessDaysCalculator.Interface;

/// <summary>
/// Interface for Holiday rule classes.
/// </summary>
public interface IHolidayRule
{
    bool IsValidHoliday(DateTime date);
}