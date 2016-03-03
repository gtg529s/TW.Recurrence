using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TW.Recurrence.MonthlyRecurrences;

namespace TW.Recurrence.Tests.Unit
{
    public class DayOfWeekMonthlyRecurrenceTests
    {
        #region Behavior Tests

        [TestClass]
        public class Behavior
        {
            [TestMethod]
            public void CanCalculateRecurrenceSet()
            {
                #region Arrange

                var validStartDateTime = new DateTime(2014, 1, 31);
                const int validMonthsBetweenOccurrences = 2;
                DateTime validEndDateTime = validStartDateTime.AddMonths(14);
                var validDayOfWeekMonthlyRecurrence = new DayOfWeekMonthlyRecurrence(validStartDateTime,
                    validMonthsBetweenOccurrences, validEndDateTime);

                var expectedRecurrenceSet = new List<DateTime>
                {
                    new DateTime(2014, 1, 31), // 1/31/2014 (January)
                    new DateTime(2014, 3, 28), // 3/28/2014 (March)
                    new DateTime(2014, 5, 30), // 5/30/2014 (May)
                    new DateTime(2014, 7, 25), // 7/25/2014 (July)
                    new DateTime(2014, 9, 26), // 9/26/2014 (September)
                    new DateTime(2014, 11, 28), // 11/28/2014 (November)
                    new DateTime(2015, 1, 30), // 1/30/2015 (January)
                    new DateTime(2015, 3, 27) // 3/27/2015 (March)
                };

                #endregion

                #region Act

                IEnumerable<DateTime> recurrenceSet = validDayOfWeekMonthlyRecurrence.CalculateRecurrenceSet();

                #endregion

                #region Assert

                Assert.IsTrue(expectedRecurrenceSet.SequenceEqual(recurrenceSet));

                #endregion
            }

            [TestMethod]
            public void CalculatedRecurrenceSetPagingWorks()
            {
                #region Arrange

                var validStartDateTime = new DateTime(2014, 1, 31);
                const int validMonthsBetweenOccurrences = 2;
                DateTime validEndDateTime = validStartDateTime.AddMonths(9);
                var validDayOfWeekMonthlyRecurrence = new DayOfWeekMonthlyRecurrence(validStartDateTime,
                    validMonthsBetweenOccurrences, validEndDateTime);

                var expectedRecurrenceSet = new List<DateTime>
                {
                    new DateTime(2014, 3, 28), // 3/28/2014 (March)
                    new DateTime(2014, 5, 30), // 5/30/2014 (May)
                };

                #endregion

                #region Act

                const int validSkip = 1;
                const int validTake = 2;
                IEnumerable<DateTime> recurrenceSet = validDayOfWeekMonthlyRecurrence.CalculateRecurrenceSet(validSkip,
                    validTake);

                #endregion

                #region Assert

                Assert.IsTrue(expectedRecurrenceSet.SequenceEqual(recurrenceSet));

                #endregion
            }

            [TestMethod]
            public void CalculatedRecurrenceSetFilteringWorks()
            {
                #region Arrange

                var validStartDateTime = new DateTime(2014, 1, 31);
                const int validMonthsBetweenOccurrences = 2;
                DateTime validEndDateTime = validStartDateTime.AddMonths(9);
                var validDayOfWeekMonthlyRecurrence = new DayOfWeekMonthlyRecurrence(validStartDateTime,
                    validMonthsBetweenOccurrences, validEndDateTime);

                var expectedRecurrenceSet = new List<DateTime>
                {
                    new DateTime(2014, 3, 28), // 3/28/2014 (March)
                    new DateTime(2014, 5, 30), // 5/30/2014 (May)
                    new DateTime(2014, 7, 25), // 7/25/2014 (July)
                    new DateTime(2014, 9, 26) // 9/26/2014 (September)
                };

                #endregion

                #region Act

                var validDateTimeWindow = new DateTimeWindow(validStartDateTime.AddDays(1),
                    validEndDateTime.Subtract(TimeSpan.FromDays(1)));

                IEnumerable<DateTime> recurrenceSet =
                    validDayOfWeekMonthlyRecurrence.CalculateRecurrenceSet(dateTimeWindow: validDateTimeWindow);

                #endregion

                #region Assert

                Assert.IsTrue(expectedRecurrenceSet.SequenceEqual(recurrenceSet));

                #endregion
            }

            [TestMethod]
            public void CalculatedRecurrenceSetEnforcesExcludedDates()
            {
                #region Arrange

                var validStartDateTime = new DateTime(2014, 1, 31);
                const int validMonthsBetweenOccurrences = 2;
                DateTime validEndDateTime = validStartDateTime.AddMonths(9);
                var validDayOfWeekMonthlyRecurrence = new DayOfWeekMonthlyRecurrence(validStartDateTime,
                    validMonthsBetweenOccurrences, validEndDateTime);

                validDayOfWeekMonthlyRecurrence.Exclude(new DateTime(2014, 5, 30));
                var expectedRecurrenceSet = new List<DateTime>
                {
                    new DateTime(2014, 1, 31), // 1/31/2014 (January)
                    new DateTime(2014, 3, 28), // 3/28/2014 (March)
                    new DateTime(2014, 7, 25), // 7/25/2014 (July)
                    new DateTime(2014, 9, 26) // 9/26/2014 (September)
                };

                #endregion

                #region Act

                IEnumerable<DateTime> recurrenceSet = validDayOfWeekMonthlyRecurrence.CalculateRecurrenceSet();

                #endregion

                #region Assert

                Assert.IsTrue(expectedRecurrenceSet.SequenceEqual(recurrenceSet));

                #endregion
            }
        }

        #endregion
    }
}