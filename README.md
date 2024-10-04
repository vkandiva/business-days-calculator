# Business Day Calculator

## Overview

The **Business Day Calculator** is a C# assembly that calculates the number of business days between two dates.
It provides methods to compute:

- Weekdays between two dates: Counts only Monday through Friday, excluding weekends.
- Business days between two dates: Similar to weekdays, but also excludes specified public holidays.
- Business days between two dates (with Holiday Rules): Similar to Business days between two dates but considers complex rules for calculating holidays.

## Usage:

### Installation

Clone the repository `business-days-calculator` class into your project.

```bash
git clone https://github.com/vakndiva/business-days-calculator.git
```

### Example

```csharp
var firstDate = new DateTime(2023, 10, 1);
var secondDate = new DateTime(2023, 10, 10);
var publicHolidays = new List<DateTime> { new DateTime(2023, 10, 3) };
var businessDayCalculator = new BusinessDayCalculator();

int businessDays = businessDayCalculator.BusinessDaysBetweenTwoDates(firstDate, secondDate, publicHolidays);
Console.WriteLine($"Business Days: {businessDays}");

// Using holiday rules
List<IHolidayRule> _holidayRules = new()
                                        {
                                            new SameDayHolidayRule(12, 25), // Christmas
                                            new SameDayHolidayRule(12, 26), // Boxing Day
                                            new NextDayHolidayRule(1, 1), // New Year's Day
                                            new OccurrenceBasedHolidayRule(6, 2, DayOfWeek.Monday) // Queens Birthday
                                        };
businessDays = _ruleBasedBusinessDayFunctions.BusinessDaysBetweenTwoDates(firstDate, secondDate, _holidayRules);
Console.WriteLine($"Business Days usning Holida rules: {businessDays}");

```

## Running Tests

Run unit tests with the following command:

```bash
dotnet restore
dotnet test
```
