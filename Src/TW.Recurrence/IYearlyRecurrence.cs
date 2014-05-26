namespace TW.Platform.UtilityTypes
{
    public interface IYearlyRecurrence : IRecurrence
    {
        #region Properties

        int YearsBetweenOccurences { get; }

        #endregion
    }
}