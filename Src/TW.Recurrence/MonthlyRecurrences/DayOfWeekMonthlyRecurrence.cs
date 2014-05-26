using System;
using System.Collections.Generic;

namespace TW.Recurrence.MonthlyRecurrences
{
    public class DayOfWeekMonthlyRecurrence : AbstractMonthlyRecurrence
    {
        #region Constructors

        public DayOfWeekMonthlyRecurrence(
            DateTime startDateTime,
            int monthsBetweenOccurrences,
            DateTime? endDateTime = null,
            IEnumerable<DateTime> excludedDateTimes = null)
            : base(startDateTime, monthsBetweenOccurrences, endDateTime, excludedDateTimes)
        {
        }

        #endregion

        #region Methods

        public override DateTime GetDateTimeOfNextOccurrenceCandidate(DateTime precedingDateTime)
        {
            int testYear = precedingDateTime.Year;
            int testMonth = precedingDateTime.Month;
            AddMonthsBetweenOccurrences(ref testYear, ref testMonth);

            int day = GetDayOfOrdinalOrLastDayOfWeekOccurrenceInMonth(
                testYear,
                testMonth,
                GetOrdinalDayOfWeekOccurrenceInMonth(StartDateTime),
                precedingDateTime.DayOfWeek);

            return new DateTime(
                testYear,
                testMonth,
                day,
                precedingDateTime.Hour,
                precedingDateTime.Minute,
                precedingDateTime.Second,
                precedingDateTime.Millisecond,
                precedingDateTime.Kind);
        }

        private static int GetOrdinalDayOfWeekOccurrenceInMonth(DateTime dateTime)
        {
            var testDateTime = new DateTime(dateTime.Year, dateTime.Month, 1);
            int dayOfWeekOccurrenceCount = 0;
            while (testDateTime <= dateTime)
            {
                if (testDateTime.DayOfWeek == dateTime.DayOfWeek)
                {
                    dayOfWeekOccurrenceCount++;
                }
                testDateTime = testDateTime.AddDays(1);
            }
            return dayOfWeekOccurrenceCount;
        }

        /// <summary>
        ///     Tries to get the day on which <paramref name="dayOfWeek" />
        ///     has occurred the <paramref name="ordinal" /> number of times in a given
        ///     <paramref name="month" /> of a given <paramref name="year" />. If the
        ///     <paramref name="dayOfWeek" /> does not occur <paramref name="ordinal" /> times then the
        ///     day of the last occurrence will be returned.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="ordinal"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        private static int GetDayOfOrdinalOrLastDayOfWeekOccurrenceInMonth(int year, int month, int ordinal,
            DayOfWeek dayOfWeek)
        {
            var testDateTime = new DateTime(year, month, 1);
            int dayOfWeekOccurrenceCount = 0;
            int dayOfLastCountedDayOfWeekOccurrence = 0;

            // within the bounds of a given month
            while (testDateTime.Month == month)
            {
                // every time we find DayOfWeek update the count and record the day it occurs on
                if (testDateTime.DayOfWeek == dayOfWeek)
                {
                    dayOfWeekOccurrenceCount++;
                    dayOfLastCountedDayOfWeekOccurrence = testDateTime.Day;
                }

                // if the DayOfWeek has occurred ordinal times we found a solution.
                if (dayOfWeekOccurrenceCount == ordinal)
                {
                    return testDateTime.Day;
                }

                // otherwise keep iterating
                testDateTime = testDateTime.AddDays(1);
            }

            // if the dayOfWeekOccurrenceCount didn't reach the desired ordinal in this month
            // then fallback to the last occurrence
            return dayOfLastCountedDayOfWeekOccurrence;
        }

        #endregion
    }
}