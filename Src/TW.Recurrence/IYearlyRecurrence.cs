namespace TW.Recurrence
{
    public interface IYearlyRecurrence : IRecurrence
    {
        #region Properties

        int YearsBetweenOccurences { get; }

        #endregion
    }
}