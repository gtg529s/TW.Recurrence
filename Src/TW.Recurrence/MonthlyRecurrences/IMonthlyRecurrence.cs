namespace TW.Platform.UtilityTypes
{
    public interface IMonthlyRecurrence : IRecurrence
    {
        #region Properties

        int MonthsBetweenOccurrences { get; }

        #endregion
    }
}