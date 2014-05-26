using System;
using System.Collections.Generic;

namespace TW.Platform.UtilityTypes
{
    public class YearlyRecurrence : AbstractRecurrence, IYearlyRecurrence
    {
        #region Constructors

        public YearlyRecurrence(
            DateTime startDateTime,
            int yearsBetweenOccurrences,
            DateTime? endDateTime = null,
            IEnumerable<DateTime> excludedDateTimes = null)
            : base(startDateTime, endDateTime, excludedDateTimes)
        {
            YearsBetweenOccurences = yearsBetweenOccurrences;
        }

        #endregion

        #region Properties

        public int YearsBetweenOccurences { get; private set; }

        #endregion

        #region Methods

        public override DateTime GetDateTimeOfNextOccurrenceCandidate(DateTime precedingDateTime)
        {
            return precedingDateTime.AddYears(YearsBetweenOccurences);
        }

        #endregion
    }
}