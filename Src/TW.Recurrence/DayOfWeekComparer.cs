using System;
using System.Collections.Generic;

namespace TW.Recurrence
{
    public class DayOfWeekComparer : IComparer<DayOfWeek>
    {
        #region Methods

        public int Compare(DayOfWeek x, DayOfWeek y)
        {
            return x - y;
        }

        #endregion
    }
}