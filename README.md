Recurrence
==========
**Continuous Integration Nuget Feed:**
A [TonightWe continuous integration Nuget feed](https://www.myget.org/feed/Packages/tonightwe) is available.  
The last build was: ![Unknown](https://www.myget.org/BuildSource/Badge/tonightwe?identifier=ee7c91c4-db73-40b9-a342-7d9aca6596de)

**Description**
A library for .Net that makes creating, managing, and working with recurrences easy

**Installation:**
The easiest way to install is by using [Nuget](http://nuget.org/packages/TW.Recurrence/)

**Examples:**
Look at the [Unit Tests](https://github.com/TonightWe/Recurrence/tree/master/Src/TW.Recurrence.Tests.Unit) for example usage of all recurrence types

**Quickstart:**
There are several recurrence types namely,
* DailyRecurrence
* DayOfMonthMonthlyRecurrence
* DayOfWeekMonthlyRecurrence
* WeeklyRecurrence
* YearlyRecurrence.

However, in the interest of keeping this quickstart short and sweet, only DailyRecurrence will be demonstrated here. That said, all of the recurrence types share the same core api so this should go a long way towards teaching you how to use the others as well.

```C#
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
