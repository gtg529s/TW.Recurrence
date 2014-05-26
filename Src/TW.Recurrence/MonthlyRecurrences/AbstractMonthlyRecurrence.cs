using System;
using System.Collections.Generic;

namespace TW.Platform.UtilityTypes
{
    public abstract class AbstractMonthlyRecurrence : AbstractRecurrence, IMonthlyRecurrence
    {
        #region Constructors

        protected AbstractMonthlyRecurrence(
            DateTime startDateTime,
            int monthsBetweenOccurrences,
            DateTime? endDateTime = null,
            IEnumerable<DateTime> excludedDateTimes = null)
            : base(startDateTime, endDateTime, excludedDateTimes)
        {
            MonthsBetweenOccurrences = monthsBetweenOccurrences;
        }

        #endregion

        #region Properties

        public int MonthsBetweenOccurrences { get; private set; }

        #endregion

        #region Methods

        protected void AddMonthsBetweenOccurrences(ref int year, ref int month)
        {
            int initialYear = year;
            int initialMonth = month;

            if (initialMonth + MonthsBetweenOccurrences > 12)
            {
                year = initialYear + 1;
                month = (initialMonth + MonthsBetweenOccurrences) - 12;
            }
            else
            {
                month = initialMonth + MonthsBetweenOccurrences;
            }
        }

        #endregion
    }
}