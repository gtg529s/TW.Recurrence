using System;
using System.Collections.Generic;

namespace TW.Platform.UtilityTypes
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
            int testYear = precedingDateTime.Year;
            int testMonth = precedingDateTime.Month;
            AddMonthsBetweenOccurrences(ref testYear, ref testMonth);


            while (DateTime.DaysInMonth(testYear, testMonth) < precedingDateTime.Day)
            {
                AddMonthsBetweenOccurrences(ref testYear, ref testMonth);
            }

            return
                precedingDateTime.AddYears(testYear - precedingDateTime.Year)
                    .AddMonths(Math.Abs(testMonth - precedingDateTime.Month));
        }

        #endregion
    }
}