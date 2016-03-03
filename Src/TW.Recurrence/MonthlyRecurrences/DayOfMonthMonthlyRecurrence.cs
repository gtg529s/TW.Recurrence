using System;
using System.Collections.Generic;

namespace TW.Recurrence.MonthlyRecurrences
{
    public class DayOfMonthMonthlyRecurrence : AbstractMonthlyRecurrence
    {
        #region Constructors

        public DayOfMonthMonthlyRecurrence(
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
            //get the next occurance by adding the months between
            precedingDateTime = precedingDateTime.AddMonths(MonthsBetweenOccurrences);

            int testYear = precedingDateTime.Year;
            int testMonth = precedingDateTime.Month;

            //If the next month doesn't contain the day we're recurring on, then skip it
            while (DateTime.DaysInMonth(testYear, testMonth) < StartDateTime.Day)
            {
                precedingDateTime = precedingDateTime.AddMonths(MonthsBetweenOccurrences);
                testYear = precedingDateTime.Year;
                testMonth = precedingDateTime.Month;
            }
            return new DateTime(precedingDateTime.Year, precedingDateTime.Month, StartDateTime.Day);
        }

        #endregion
    }
}