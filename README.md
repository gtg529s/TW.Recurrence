Recurrence
==========

**Description**
A library for .Net that makes creating, managing, and working with recurrences easy

**Installation:**
The easiest way to install is by using [Nuget](http://nuget.org/packages/TW.Recurrence/)

**Documentation:**
Documentation is available via the [Wiki](https://github.com/TonightWe/Recurrence/wiki)

**Quickstart:**

```C#
// Note: there are several recurrence types namely, 
// DailyRecurrence, DayOfMonthMonthlyRecurrence, DayOfWeekMonthlyRecurrence,
// WeeklyRecurrence,and YearlyRecurrence.In the hopes of keeping this this
// quickstart short and sweet, only DailyRecurrence will be demonstrated here.
// It should be noted however that the other recurrences all share the same
// core functionality demonstrated here.

// Part 1: Create A Recurrence
var startDateTime = DateTime.UtcNow;
var daysBetweenOccurrences = 3;
var endDateTime = startDateTime.AddDays(daysBetweenOccurrences*3)
var dailyRecurrence = new DailyRecurrence(startDateTime,daysBetweenOccurrences,endDateTime);

// Part 2: Calculate A Recurrence Set
recurrenceSet = dailyRecurrence.CalculateRecurrenceSet();
/*
recurrenceSet would contain:
startDateTime
startDateTime.AddDays(daysBetweenOccurrences*1)
startDateTime.AddDays(daysBetweenOccurrences*2)
endDateTime
*/

// Part 3: Paging A Recurrence Set
var skip = 1;
var take = 2;
recurrenceSet = dailyRecurrence.CalculateRecurrenceSet(skip,take);
/*
recurrenceSet would contain:
startDateTime.AddDays(daysBetweenOccurrences*1)
startDateTime.AddDays(daysBetweenOccurrences*2)
*/

// Part 4: Filtering A Recurrence Set
var dateTimeWindow = new DateTimeWindow(startDateTime.AddDays(1),endDateTime.AddDays(-1));
recurrenceSet = dailyRecurrence.CalculateRecurrenceSet(dateTimeWindow:dateTimeWindow);
/*
recurrenceSet would contain:
startDateTime.AddDays(daysBetweenOccurrences*1)
startDateTime.AddDays(daysBetweenOccurrences*2)
*/

// part 5: Exclude Dates
var excludedDate = DateTime.UtcNow.AddDays(daysBetweenOccurrences);
dailyRecurrence.exclude(excludedDate);
recurrenceSet = dailyRecurrence.CalculateRecurrenceSet();
/*
recurrenceSet would contain:
startDateTime
startDateTime.AddDays(daysBetweenOccurrences*2)
endDateTime
*/
```
