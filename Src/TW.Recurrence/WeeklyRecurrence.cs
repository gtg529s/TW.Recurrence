using System;
using System.Collections.Generic;
using System.Linq;

namespace TW.Recurrence
{
    public class WeeklyRecurrence : AbstractRecurrence, IWeeklyRecurrence
    {
        #region Constructors

        public WeeklyRecurrence(
            DateTime startDateTime,
            int weeksBetweenOccurrences,
            IEnumerable<DayOfWeek> includedWeekDays,
            DateTime? endDateTime = null,
            IEnumerable<DateTime> excludedDateTimes = null)
            : base(startDateTime, endDateTime, excludedDateTimes)
        {
            WeeksBetweenOccurences = weeksBetweenOccurrences;
            _includedWeekDays =
                new SortedList<DayOfWeek, DayOfWeek>(includedWeekDays.ToDictionary(dayOfWeek => dayOfWeek),
                    new DayOfWeekComparer());
        }

        #endregion

        #region Fields

        private readonly SortedList<DayOfWeek, DayOfWeek> _includedWeekDays;

        #endregion

        #region Properties

        private int DaysBetweenOccurrences
        {
            get { return WeeksBetweenOccurences*7; }
        }

        public int WeeksBetweenOccurences { get; private set; }

        public IEnumerable<DayOfWeek> IncludedWeekDays
        {
            get { return _includedWeekDays.Values; }
        }

        #endregion

        #region Methods

        public override DateTime GetDateTimeOfNextOccurrenceCandidate(DateTime precedingDateTime)
        {
            DateTime nextDateTimeCandidate;
            DayOfWeek nextDayOfWeekCandidate;
            // first try to find a weekday included in the current weekly occurrence. 
            if (TryGetDayOfWeekOfNextOccurrenceCandidate(precedingDateTime.DayOfWeek, out nextDayOfWeekCandidate))
            {
                nextDateTimeCandidate = FastForwardDateTimeToDayOfWeek(precedingDateTime, nextDayOfWeekCandidate);
                return nextDateTimeCandidate;
            }

            // If we made it here, all included days of the current weekly occurrence have been exhausted;
            // begin looking at Monday of the next weekly occurrence
            nextDateTimeCandidate =
                FastForwardDateTimeToDayOfWeek(precedingDateTime, DayOfWeek.Monday).AddDays(DaysBetweenOccurrences);

            // if Monday is included in the next weekly occurrence then we're done
            if (IncludedWeekDays.Any(dayOfWeek => dayOfWeek == nextDateTimeCandidate.DayOfWeek))
            {
                return nextDateTimeCandidate;
            }
            // otherwise find the next included day
            nextDayOfWeekCandidate = GetNextDayOfWeekCandidate(nextDateTimeCandidate.DayOfWeek);
            return FastForwardDateTimeToDayOfWeek(nextDateTimeCandidate, nextDayOfWeekCandidate);
        }

        /// <summary>
        ///     Tries to find the next occurrence candidate taking place in the same week and occurring after
        ///     <paramref name="precedingDayOfWeek" />.
        ///     It is only a candidate because paging, filtering, exclusions, and the end of the recurrence are not taken into
        ///     account
        ///     by this method.
        /// </summary>
        /// <param name="precedingDayOfWeek"></param>
        /// <param name="nextDayOfWeekCandidate"></param>
        /// <returns></returns>
        private bool TryGetDayOfWeekOfNextOccurrenceCandidate(DayOfWeek precedingDayOfWeek,
            out DayOfWeek nextDayOfWeekCandidate)
        {
            // initialize test occurrence to the precedingOccurrence
            DayOfWeek testDayOfWeek = precedingDayOfWeek;

            // only look within the confinements of a single week
            while (testDayOfWeek != DayOfWeek.Saturday)
            {
                // advance value of testDayOfWeek by a day
                testDayOfWeek = testDayOfWeek + 1;

                // if current testDayOfWeek isn't in the set of included week days then it is not a solution; keep looking
                if (IncludedWeekDays.All(dayOfWeek => dayOfWeek != testDayOfWeek))
                {
                    continue;
                }

                // otherwise testDayOfWeek is a solution
                nextDayOfWeekCandidate = testDayOfWeek;
                return true;
            }

            nextDayOfWeekCandidate = default(DayOfWeek);
            return false;
        }

        private DayOfWeek GetNextDayOfWeekCandidate(DayOfWeek precedingDayOfWeek)
        {
            DayOfWeek nextDayOfWeekCandidate;
            if (!TryGetDayOfWeekOfNextOccurrenceCandidate(precedingDayOfWeek, out nextDayOfWeekCandidate))
            {
                throw new Exception("there are no proceeding days included in the given week");
            }
            return nextDayOfWeekCandidate;
        }

        /// <summary>
        ///     Shifts a <see cref="DateTime" /> forward in time to the next <see cref="DateTime" /> occurring with
        ///     <paramref name="newDayOfWeek" />
        /// </summary>
        /// <param name="initialDateTime"></param>
        /// <param name="newDayOfWeek"></param>
        /// <returns></returns>
        private DateTime FastForwardDateTimeToDayOfWeek(DateTime initialDateTime, DayOfWeek newDayOfWeek)
        {
            DateTime testDateTime = initialDateTime.AddDays(1);
            while (testDateTime.DayOfWeek != newDayOfWeek)
            {
                testDateTime = testDateTime.AddDays(1);
            }

            return testDateTime;
        }

        #endregion
    }
}