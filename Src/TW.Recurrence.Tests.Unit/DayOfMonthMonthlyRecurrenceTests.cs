﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TW.Recurrence.MonthlyRecurrences;

namespace TW.Recurrence.Tests.Unit
{
    public class DayOfMonthMonthlyRecurrenceTests
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
                DateTime validEndDateTime = validStartDateTime.AddMonths(12);
                var validDayOfMonthMonthlyRecurrence = new DayOfMonthMonthlyRecurrence(validStartDateTime,
                    validMonthsBetweenOccurrences, validEndDateTime);

                var expectedRecurrenceSet = new List<DateTime>
                {
                    validStartDateTime, // 1/31/2014 (January)
                    validStartDateTime.AddMonths(2), // 3/31/2014 (March)
                    validStartDateTime.AddMonths(4), // 5/31/2014 (May)
                    validStartDateTime.AddMonths(6), // 7/31/2014 (July)
                    validStartDateTime.AddMonths(12) // 1/31/2015 (Jan)
                    // no 9/31/2014 (September) because September only has 30 days so its skipped
                };

                #endregion

                #region Act

                IEnumerable<DateTime> recurrenceSet = validDayOfMonthMonthlyRecurrence.CalculateRecurrenceSet();

                #endregion

                #region Assert

                Assert.IsTrue(expectedRecurrenceSet.SequenceEqual(recurrenceSet));

                #endregion
            }

            [TestMethod]
            public void CanCalculateRecurrenceSet_2()
            {
                #region Arrange

                var validStartDateTime = new DateTime(2014, 1, 15);
                const int validMonthsBetweenOccurrences = 1;
                DateTime validEndDateTime = validStartDateTime.AddMonths(12);
                var validDayOfMonthMonthlyRecurrence = new DayOfMonthMonthlyRecurrence(validStartDateTime,
                    validMonthsBetweenOccurrences, validEndDateTime);

                var expectedRecurrenceSet = new List<DateTime>
                {
                    validStartDateTime, // 1/31/2014 (January)
                    validStartDateTime.AddMonths(1), // 3/31/2014 (March)
                    validStartDateTime.AddMonths(2), // 5/31/2014 (May)
                    validStartDateTime.AddMonths(3), // 7/31/2014 (July)
                    validStartDateTime.AddMonths(4), // 1/31/2015 (Jan)
                    validStartDateTime.AddMonths(5), // 3/31/2014 (March)
                    validStartDateTime.AddMonths(6), // 5/31/2014 (May)
                    validStartDateTime.AddMonths(7), // 7/31/2014 (July)
                    validStartDateTime.AddMonths(8), // 1/31/2015 (Jan)
                    validStartDateTime.AddMonths(9), // 3/31/2014 (March)
                    validStartDateTime.AddMonths(10), // 5/31/2014 (May)
                    validStartDateTime.AddMonths(11), // 7/31/2014 (July)
                    validStartDateTime.AddMonths(12) // 1/31/2015 (Jan)
                    // no 9/31/2014 (September) because September only has 30 days so its skipped
                };

                #endregion

                #region Act

                IEnumerable<DateTime> recurrenceSet = validDayOfMonthMonthlyRecurrence.CalculateRecurrenceSet();

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
                var validDayOfMonthMonthlyRecurrence = new DayOfMonthMonthlyRecurrence(validStartDateTime,
                    validMonthsBetweenOccurrences, validEndDateTime);

                var expectedRecurrenceSet = new List<DateTime>
                {
                    validStartDateTime.AddMonths(2), // 3/31/2014 (March)
                    validStartDateTime.AddMonths(4) // 5/31/2014 (May)
                };

                #endregion

                #region Act

                const int validSkip = 1;
                const int validTake = 2;
                IEnumerable<DateTime> recurrenceSet = validDayOfMonthMonthlyRecurrence.CalculateRecurrenceSet(
                    validSkip, validTake);

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
                var validDayOfMonthMonthlyRecurrence = new DayOfMonthMonthlyRecurrence(validStartDateTime,
                    validMonthsBetweenOccurrences, validEndDateTime);

                var expectedRecurrenceSet = new List<DateTime>
                {
                    validStartDateTime.AddMonths(2), // 3/31/2014 (March)
                    validStartDateTime.AddMonths(4), // 5/31/2014 (May)
                    validStartDateTime.AddMonths(6), // 7/31/2014 (July)
                };

                #endregion

                #region Act

                var validDateTimeWindow = new DateTimeWindow(validStartDateTime.AddDays(1),
                    validEndDateTime.Subtract(TimeSpan.FromDays(1)));

                IEnumerable<DateTime> recurrenceSet =
                    validDayOfMonthMonthlyRecurrence.CalculateRecurrenceSet(dateTimeWindow: validDateTimeWindow);

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
                var validDayOfMonthMonthlyRecurrence = new DayOfMonthMonthlyRecurrence(validStartDateTime,
                    validMonthsBetweenOccurrences, validEndDateTime);

                validDayOfMonthMonthlyRecurrence.Exclude(validStartDateTime.AddMonths(2));

                var expectedRecurrenceSet = new List<DateTime>
                {
                    validStartDateTime, // 1/31/2014 (January)
                    validStartDateTime.AddMonths(4), // 5/31/2014 (May)
                    validStartDateTime.AddMonths(6), // 7/31/2014 (July)
                };

                #endregion

                #region Act

                IEnumerable<DateTime> recurrenceSet = validDayOfMonthMonthlyRecurrence.CalculateRecurrenceSet();

                #endregion

                #region Assert

                Assert.IsTrue(expectedRecurrenceSet.SequenceEqual(recurrenceSet));

                #endregion
            }
        }

        #endregion
    }
}