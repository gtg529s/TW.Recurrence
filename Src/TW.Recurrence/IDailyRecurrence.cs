﻿namespace TW.Recurrence
{
    /// <summary>
    ///     A recurrence that occurs daily
    /// </summary>
    public interface IDailyRecurrence : IRecurrence
    {
        #region Properties

        int DaysBetweenOccurences { get; }

        #endregion
    }
}