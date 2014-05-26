using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TW.Recurrence.Tests.Unit
{
    public class DailyRecurrenceTests
    {
        #region Behavior Tests

        [TestClass]
        public class Behavior
        {
            [TestMethod]
            public void CanCalculateRecurrenceSet()
            {
                #region Arrange

                var validStartDateTime = new DateTime(2014, 2, 18);
                const int validDaysBetweenOccurrences = 3;
                var validEndDateTime = validStartDateTime.AddDays(validDaysBetweenOccurrences*3);
                var validDailyRecurrence = new DailyRecurrence(validStartDateTime, validDaysBetweenOccurrences,
                    validEndDateTime);

                #endregion

                #region Act

                var recurrenceSet = validDailyRecurrence.CalculateRecurrenceSet();

                #endregion

                #region Assert

                var expectedRecurrenceSet = new List<DateTime>
                {
                    validStartDateTime,
                    validStartDateTime.AddDays(validDaysBetweenOccurrences*1),
                    validStartDateTime.AddDays(validDaysBetweenOccurrences*2),
                    validStartDateTime.AddDays(validDaysBetweenOccurrences*3)
                };

                Assert.IsTrue(expectedRecurrenceSet.SequenceEqual(recurrenceSet));

                #endregion
            }

            [TestMethod]
            public void CalculatedRecurrenceSetPagingWorks()
            {
                #region Arrange

                var validStartDateTime = new DateTime(2014, 2, 18);
                const int validDaysBetweenOccurrences = 3;
                var validEndDateTime = validStartDateTime.AddDays(9);
                var validDailyRecurrence = new DailyRecurrence(validStartDateTime, validDaysBetweenOccurrences,
                    validEndDateTime);

                #endregion

                #region Act

                const int validSkip = 1;
                const int validTake = 2;
                var recurrenceSet = validDailyRecurrence.CalculateRecurrenceSet(validSkip, validTake);

                #endregion

                #region Assert

                var expectedRecurrenceSet = new List<DateTime>
                {
                    validStartDateTime.AddDays(validDaysBetweenOccurrences*1),
                    validStartDateTime.AddDays(validDaysBetweenOccurrences*2)
                };

                Assert.IsTrue(expectedRecurrenceSet.SequenceEqual(recurrenceSet));

                #endregion
            }

            [TestMethod]
            public void CalculatedRecurrenceSetFilteringWorks()
            {
                #region Arrange

                var validStartDateTime = new DateTime(2014, 2, 18);
                const int validDaysBetweenOccurrences = 3;
                var validEndDateTime = validStartDateTime.AddDays(9);
                var validDailyRecurrence = new DailyRecurrence(validStartDateTime, validDaysBetweenOccurrences,
                    validEndDateTime);

                #endregion

                #region Act

                var validDateTimeWindow = new DateTimeWindow(validStartDateTime.AddDays(1),
                    validEndDateTime.Subtract(TimeSpan.FromDays(1)));

                var recurrenceSet =
                    validDailyRecurrence.CalculateRecurrenceSet(dateTimeWindow: validDateTimeWindow);

                #endregion

                #region Assert

                var expectedRecurrenceSet = new List<DateTime>
                {
                    validStartDateTime.AddDays(validDaysBetweenOccurrences*1),
                    validStartDateTime.AddDays(validDaysBetweenOccurrences*2)
                };

                Assert.IsTrue(expectedRecurrenceSet.SequenceEqual(recurrenceSet));

                #endregion
            }

            [TestMethod]
            public void CalculatedRecurrenceSetEnforcesExcludedDates()
            {
                #region Arrange

                var validStartDateTime = new DateTime(2014, 2, 18);
                const int validDaysBetweenOccurrences = 3;
                var validEndDateTime = validStartDateTime.AddDays(9);
                var validDailyRecurrence = new DailyRecurrence(validStartDateTime, validDaysBetweenOccurrences,
                    validEndDateTime);
                validDailyRecurrence.Exclude(validStartDateTime.AddDays(validDaysBetweenOccurrences*2));

                #endregion

                #region Act

                var recurrenceSet = validDailyRecurrence.CalculateRecurrenceSet();

                #endregion

                #region Assert

                var expectedRecurrenceSet = new List<DateTime>
                {
                    validStartDateTime,
                    validStartDateTime.AddDays(validDaysBetweenOccurrences*1),
                    validStartDateTime.AddDays(validDaysBetweenOccurrences*3)
                };

                Assert.IsTrue(expectedRecurrenceSet.SequenceEqual(recurrenceSet));

                #endregion
            }
        }

        #endregion
    }
}