using System;
using System.Collections.Generic;
using System.Linq;
using Seterlund.CodeGuard;

namespace TW.Platform.UtilityTypes
{
    public abstract class AbstractRecurrence : IRecurrence
    {
        #region Constructors

        protected AbstractRecurrence(DateTime startDateTime, DateTime? endDateTime = null,
            IEnumerable<DateTime> excludedDateTimes = null)
        {
            StartDateTime = Guard.That(startDateTime).IsNotDefault().Value;
            EndDateTime = endDateTime ?? DateTime.MaxValue;
            _excludedDates = (null == excludedDateTimes) ? new List<DateTime>() : excludedDateTimes.ToList();
        }

        #endregion

        #region Fields

        private readonly List<DateTime> _excludedDates;

        #endregion

        #region Properties

        public IEnumerable<DateTime> ExcludedDates
        {
            get { return _excludedDates; }
        }

        public DateTime StartDateTime { get; private set; }
        public DateTime EndDateTime { get; private set; }

        #endregion

        #region Methods

        public void Exclude(DateTime excludedDateTime)
        {
            if (!ExcludedDates.Any(_excludedDateTime => _excludedDateTime == excludedDateTime))
            {
                _excludedDates.Add(excludedDateTime);
            }
        }

        public IEnumerable<DateTime> CalculateRecurrenceSet(int? skip = 0, int take = int.MaxValue,
            DateTimeWindow dateTimeWindow = null)
        {
            // if dateTimeWindow wasn't passed; default is all time
            dateTimeWindow = dateTimeWindow ?? DateTimeWindow.AllTime;
            var recurrenceSet = new List<DateTime>();
            DateTime precedingOccurrence = StartDateTime;
            DateTime nextOccurrence;

            // handle skipping a number of occurrences into the complete recurrence set
            if (skip > 0)
            {
                // initialize to 1 because StartDateTime is used to initialize precedingOccurrence
                int skipCount = 1;
                while (skipCount != skip)
                {
                    if (TryCalculateNextOccurrence(precedingOccurrence, dateTimeWindow, out nextOccurrence))
                    {
                        precedingOccurrence = nextOccurrence;
                        skipCount++;
                    }
                        // handle skipping more results than the number of results which exist
                    else
                    {
                        return recurrenceSet;
                    }
                }
            }
            else if (dateTimeWindow.MinimumIncludedDateTime < StartDateTime)
            {
                recurrenceSet.Add(StartDateTime);
            }

            while (TryCalculateNextOccurrence(precedingOccurrence, dateTimeWindow, out nextOccurrence))
            {
                precedingOccurrence = nextOccurrence;
                recurrenceSet.Add(nextOccurrence);
                // handle taking only a subset of the complete recurrence set
                if (recurrenceSet.Count == take)
                {
                    break;
                }
            }
            return recurrenceSet;
        }

        public bool TryCalculateNextOccurrence(DateTime precedingOccurrence, DateTimeWindow dateTimeWindow,
            out DateTime nextOccurrence)
        {
            DateTime nextDateTimeCandidate = GetDateTimeOfNextOccurrenceCandidate(precedingOccurrence);
            while (!IsDateTimeAfterEndDateTime(nextDateTimeCandidate) &&
                   !IsDateTimeAfterDateTimeWindow(nextDateTimeCandidate, dateTimeWindow))
            {
                if (!IsDateExcluded(nextDateTimeCandidate) &&
                    !IsDateTimeBeforeDateTimeWindow(nextDateTimeCandidate, dateTimeWindow))
                {
                    nextOccurrence = nextDateTimeCandidate;
                    return true;
                }
                nextDateTimeCandidate = GetDateTimeOfNextOccurrenceCandidate(nextDateTimeCandidate);
            }
            nextOccurrence = default(DateTime);
            return false;
        }

        public abstract DateTime GetDateTimeOfNextOccurrenceCandidate(DateTime precedingDateTime);

        private bool IsDateTimeBeforeDateTimeWindow(DateTime dateTime, DateTimeWindow dateTimeWindow)
        {
            return dateTimeWindow.MinimumIncludedDateTime > dateTime;
        }

        private bool IsDateTimeAfterDateTimeWindow(DateTime dateTime, DateTimeWindow dateTimeWindow)
        {
            return dateTimeWindow.MaximumIncludedDateTime < dateTime;
        }

        private bool IsDateExcluded(DateTime dateTime)
        {
            return ExcludedDates.Any(excludedDate => excludedDate.Date == dateTime.Date);
        }

        private bool IsDateTimeAfterEndDateTime(DateTime dateTime)
        {
            return EndDateTime < dateTime;
        }

        #endregion
    }
}