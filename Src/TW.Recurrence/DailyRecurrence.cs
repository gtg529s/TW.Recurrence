using System;
using System.Collections.Generic;

namespace TW.Recurrence
{
    public class DailyRecurrence : AbstractRecurrence, IDailyRecurrence
    {
        #region Constructors

        public DailyRecurrence(
            DateTime startDateTime,
            int daysBetweenOccurrences,
            DateTime? endDateTime = null,
            IEnumerable<DateTime> excludedDateTimes = null)
            : base(startDateTime, endDateTime, excludedDateTimes)
        {
            DaysBetweenOccurences = daysBetweenOccurrences;
        }

        #endregion

        #region Properties

        public int DaysBetweenOccurences { get; private set; }

        #endregion

        #region Methods

        public override DateTime GetDateTimeOfNextOccurrenceCandidate(DateTime precedingDateTime)
        {
            return precedingDateTime.AddDays(DaysBetweenOccurences);
        }

        #endregion
    }
}