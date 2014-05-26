using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TW.Recurrence.Tests.Unit
{
    public class YearlyRecurrenceTests
    {
        #region Behavior Tests

        [TestClass]
        public class Behavior
        {
            [TestMethod]
            public void CanCalculateRecurrenceSet()
            {
                #region Arrange

                var validStartDateTime = new DateTime(2014, 2, 19);
                const int validYearsBetweenOccurrences = 2;
                DateTime validEndDateTime = validStartDateTime.AddYears(6);
                var validYearlyRecurrence = new YearlyRecurrence(validStartDateTime, validYearsBetweenOccurrences,
                    validEndDateTime);

                #endregion

                #region Act

                IEnumerable<DateTime> recurrenceSet = validYearlyRecurrence.CalculateRecurrenceSet();

                #endregion

                #region Assert

                var expectedRecurrenceSet = new List<DateTime>
                {
                    validStartDateTime,
                    validStartDateTime.AddYears(validYearsBetweenOccurrences*1),
                    validStartDateTime.AddYears(validYearsBetweenOccurrences*2),
                    validStartDateTime.AddYears(validYearsBetweenOccurrences*3)
                };

                Assert.IsTrue(expectedRecurrenceSet.SequenceEqual(recurrenceSet));

                #endregion
            }

            [TestMethod]
            public void CalculatedRecurrenceSetPagingWorks()
            {
                #region Arrange

                var validStartDateTime = new DateTime(2014, 2, 19);
                const int validYearsBetweenOccurrences = 2;
                DateTime validEndDateTime = validStartDateTime.AddYears(6);
                var validYearlyRecurrence = new YearlyRecurrence(validStartDateTime, validYearsBetweenOccurrences,
                    validEndDateTime);

                #endregion

                #region Act

                const int validSkip = 1;
                const int validTake = 2;
                IEnumerable<DateTime> recurrenceSet = validYearlyRecurrence.CalculateRecurrenceSet(validSkip, validTake);

                #endregion

                #region Assert

                var expectedRecurrenceSet = new List<DateTime>
                {
                    validStartDateTime.AddYears(validYearsBetweenOccurrences*1),
                    validStartDateTime.AddYears(validYearsBetweenOccurrences*2)
                };

                Assert.IsTrue(expectedRecurrenceSet.SequenceEqual(recurrenceSet));

                #endregion
            }

            [TestMethod]
            public void CalculatedRecurrenceSetFilteringWorks()
            {
                #region Arrange

                var validStartDateTime = new DateTime(2014, 2, 19);
                const int validYearsBetweenOccurrences = 2;
                DateTime validEndDateTime = validStartDateTime.AddYears(6);
                var validYearlyRecurrence = new YearlyRecurrence(validStartDateTime, validYearsBetweenOccurrences,
                    validEndDateTime);

                #endregion

                #region Act

                var validDateTimeWindow = new DateTimeWindow(validStartDateTime + TimeSpan.FromDays(1),
                    validEndDateTime - TimeSpan.FromDays(1));

                IEnumerable<DateTime> recurrenceSet =
                    validYearlyRecurrence.CalculateRecurrenceSet(dateTimeWindow: validDateTimeWindow);

                #endregion

                #region Assert

                var expectedRecurrenceSet = new List<DateTime>
                {
                    validStartDateTime.AddYears(validYearsBetweenOccurrences*1),
                    validStartDateTime.AddYears(validYearsBetweenOccurrences*2)
                };

                Assert.IsTrue(expectedRecurrenceSet.SequenceEqual(recurrenceSet));

                #endregion
            }

            [TestMethod]
            public void CalculatedRecurrenceSetEnforcesExcludedDates()
            {
                #region Arrange

                var validStartDateTime = new DateTime(2014, 2, 19);
                const int validYearsBetweenOccurrences = 2;
                DateTime validEndDateTime = validStartDateTime.AddYears(6);
                var validYearlyRecurrence = new YearlyRecurrence(validStartDateTime, validYearsBetweenOccurrences,
                    validEndDateTime);
                validYearlyRecurrence.Exclude(validStartDateTime.AddYears(validYearsBetweenOccurrences*1));

                #endregion

                #region Act

                IEnumerable<DateTime> recurrenceSet = validYearlyRecurrence.CalculateRecurrenceSet();

                #endregion

                #region Assert

                var expectedRecurrenceSet = new List<DateTime>
                {
                    validStartDateTime,
                    validStartDateTime.AddYears(validYearsBetweenOccurrences*2),
                    validStartDateTime.AddYears(validYearsBetweenOccurrences*3)
                };

                Assert.IsTrue(expectedRecurrenceSet.SequenceEqual(recurrenceSet));

                #endregion
            }
        }

        #endregion
    }
}