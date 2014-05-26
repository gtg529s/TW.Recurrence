using System;
using Seterlund.CodeGuard;

namespace TW.Recurrence
{
    public class DateTimeWindow
    {
        #region Constructors

        public DateTimeWindow(DateTime minimumIncludedDateTime, DateTime maximumIncludedDateTime)
        {
            MinimumIncludedDateTime = Guard.That(minimumIncludedDateTime).IsLessThan(maximumIncludedDateTime).Value;
            MaximumIncludedDateTime = maximumIncludedDateTime;
        }

        #endregion

        #region Properties

        public static DateTimeWindow AllTime
        {
            get { return new DateTimeWindow(DateTime.MinValue, DateTime.MaxValue); }
        }

        public DateTime MinimumIncludedDateTime { get; private set; }
        public DateTime MaximumIncludedDateTime { get; private set; }

        #endregion
    }
}