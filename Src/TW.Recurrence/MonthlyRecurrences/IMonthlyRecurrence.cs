namespace TW.Recurrence.MonthlyRecurrences
{
    public interface IMonthlyRecurrence : IRecurrence
    {
        #region Properties

        int MonthsBetweenOccurrences { get; }

        #endregion
    }
}