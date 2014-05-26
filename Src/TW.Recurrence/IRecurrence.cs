using System;
using System.Collections.Generic;

namespace TW.Recurrence
{
    public interface IRecurrence
    {
        #region Properties

        /// <summary>
        ///     The <see cref="DateTime" /> that this <see cref="IRecurrence" /> starts
        /// </summary>
        DateTime StartDateTime { get; }

        /// <summary>
        ///     The <see cref="DateTime" /> at which this <see cref="IRecurrence" /> Ends
        /// </summary>
        DateTime EndDateTime { get; }

        IEnumerable<DateTime> ExcludedDates { get; }

        #endregion

        #region Methods

        void Exclude(DateTime excludedDateTime);

        IEnumerable<DateTime> CalculateRecurrenceSet(int? skip = 0, int take = int.MaxValue,
            DateTimeWindow dateTimeWindow = null);

        #endregion
    }
}