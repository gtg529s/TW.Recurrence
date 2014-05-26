using System;
using System.Collections.Generic;

namespace TW.Platform.UtilityTypes
{
    public interface IWeeklyRecurrence : IRecurrence
    {
        #region Properties

        int WeeksBetweenOccurences { get; }
        IEnumerable<DayOfWeek> IncludedWeekDays { get; }

        #endregion
    }
}