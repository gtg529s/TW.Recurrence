using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TW.Recurrence.Tests.Unit
{
    public class WeekyRecurrenceTests
    {
        #region Behavior Tests

        [TestClass]
        public class Behavior
        {
            [TestMethod]
            public void CanCalculateRecurrenceSet()
            {
                #region Arrange

                var validStartDateTime = new DateTime(2014, 2, 17);
                const int validWeeksBetweenOccurrences = 1;
                var validIncludedWeekDays = new List<DayOfWeek>
                {
                    DayOfWeek.Monday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Saturday
                };
                // end on Saturday two weeks from the start date
                DateTime validEndDateTime =
                    validStartDateTime.AddDays((DayOfWeek.Saturday - DayOfWeek.Monday)).AddDays(7*2);
                var validWeeklyRecurrence = new WeeklyRecurrence(validStartDateTime, validWeeksBetweenOccurrences,
                    validIncludedWeekDays, validEndDateTime);

                var expectedRecurrenceSet = new List<DateTime>
                {
                    // DateTimes from first occurrence
                    validStartDateTime,
                    validStartDateTime.AddDays(DayOfWeek.Wednesday - DayOfWeek.Monday),
                    validStartDateTime.AddDays(DayOfWeek.Saturday - DayOfWeek.Monday),
                    // DateTimes from second occurrence
                    validStartDateTime.AddDays(7*2),
                    validStartDateTime.AddDays(DayOfWeek.Wednesday - DayOfWeek.Monday).AddDays(7*2),
                    validStartDateTime.AddDays(DayOfWeek.Saturday - DayOfWeek.Monday).AddDays(7*2)
                };

                #endregion

                #region Act

                IEnumerable<DateTime> recurrenceSet = validWeeklyRecurrence.CalculateRecurrenceSet();

                #endregion

                #region Assert

                Assert.IsTrue(expectedRecurrenceSet.SequenceEqual(recurrenceSet));

                #endregion
            }

            [TestMethod]
            public void CalculatedRecurrenceSetPagingWorks()
            {
                #region Arrange

                var validStartDateTime = new DateTime(2014, 2, 17);
                const int validWeeksBetweenOccurrences = 1;
                var validIncludedWeekDays = new List<DayOfWeek>
                {
                    DayOfWeek.Monday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Saturday
                };
                // end on Saturday two weeks from the start date
                DateTime validEndDateTime =
                    validStartDateTime.AddDays(DayOfWeek.Saturday - DayOfWeek.Monday).AddDays(7*2);
                var validWeeklyRecurrence = new WeeklyRecurrence(validStartDateTime, validWeeksBetweenOccurrences,
                    validIncludedWeekDays, validEndDateTime);

                var expectedRecurrenceSet = new List<DateTime>
                {
                    // DateTimes from first occurrence
                    validStartDateTime.AddDays(DayOfWeek.Wednesday - DayOfWeek.Monday),
                    validStartDateTime.AddDays(DayOfWeek.Saturday - DayOfWeek.Monday)
                };

                #endregion

                #region Act

                const int validSkip = 1;
                const int validTake = 2;
                IEnumerable<DateTime> recurrenceSet = validWeeklyRecurrence.CalculateRecurrenceSet(validSkip, validTake);

                #endregion

                #region Assert

                Assert.IsTrue(expectedRecurrenceSet.SequenceEqual(recurrenceSet));

                #endregion
            }

            [TestMethod]
            public void CalculatedRecurrenceSetFilteringWorks()
            {
                #region Arrange

                var validStartDateTime = new DateTime(2014, 2, 17);
                const int validWeeksBetweenOccurrences = 1;
                var validIncludedWeekDays = new List<DayOfWeek>
                {
                    DayOfWeek.Monday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Saturday
                };
                // end on Saturday two weeks from the start date
                DateTime validEndDateTime =
                    validStartDateTime.AddDays(DayOfWeek.Saturday - DayOfWeek.Monday).AddDays(7*2);
                var validWeeklyRecurrence = new WeeklyRecurrence(validStartDateTime, validWeeksBetweenOccurrences,
                    validIncludedWeekDays, validEndDateTime);

                var expectedRecurrenceSet = new List<DateTime>
                {
                    // DateTimes from first occurrence
                    validStartDateTime.AddDays(DayOfWeek.Wednesday - DayOfWeek.Monday),
                    validStartDateTime.AddDays(DayOfWeek.Saturday - DayOfWeek.Monday),
                    // DateTimes from second occurrence
                    validStartDateTime.AddDays(7*2),
                    validStartDateTime.AddDays(DayOfWeek.Wednesday - DayOfWeek.Monday).AddDays(7*2)
                };

                #endregion

                #region Act

                var validDateTimeWindow = new DateTimeWindow(validStartDateTime.AddDays(1),
                    validEndDateTime.Subtract(TimeSpan.FromDays(1)));

                IEnumerable<DateTime> recurrenceSet =
                    validWeeklyRecurrence.CalculateRecurrenceSet(dateTimeWindow: validDateTimeWindow);

                #endregion

                #region Assert

                Assert.IsTrue(expectedRecurrenceSet.SequenceEqual(recurrenceSet));

                #endregion
            }

            [TestMethod]
            public void CalculatedRecurrenceSetEnforcesExcludedDates()
            {
                #region Arrange

                var validStartDateTime = new DateTime(2014, 2, 17);
                const int validWeeksBetweenOccurrences = 1;
                var validIncludedWeekDays = new List<DayOfWeek>
                {
                    DayOfWeek.Monday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Saturday
                };
                // end on Saturday two weeks from the start date
                DateTime validEndDateTime =
                    validStartDateTime.AddDays(DayOfWeek.Saturday - DayOfWeek.Monday).AddDays(7*2);
                var validWeeklyRecurrence = new WeeklyRecurrence(validStartDateTime, validWeeksBetweenOccurrences,
                    validIncludedWeekDays, validEndDateTime);

                validWeeklyRecurrence.Exclude(new DateTime(2014, 2, 22));

                #endregion

                #region Act

                IEnumerable<DateTime> recurrenceSet = validWeeklyRecurrence.CalculateRecurrenceSet();

                #endregion

                #region Assert

                var expectedRecurrenceSet = new List<DateTime>
                {
                    // DateTimes from first occurrence
                    validStartDateTime,
                    validStartDateTime.AddDays(DayOfWeek.Wednesday - DayOfWeek.Monday),
                    // DateTimes from second occurrence
                    validStartDateTime.AddDays(7*2),
                    validStartDateTime.AddDays(DayOfWeek.Wednesday - DayOfWeek.Monday).AddDays(7*2),
                    validStartDateTime.AddDays(DayOfWeek.Saturday - DayOfWeek.Monday).AddDays(7*2)
                };

                Assert.IsTrue(expectedRecurrenceSet.SequenceEqual(recurrenceSet));

                #endregion
            }
        }

        #endregion
    }
}