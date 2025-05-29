using PlantCareBuddy.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlantCareBuddy.Domain.ValueObjects
{
    public class RecurrencePattern
    {
        #region Properties
        public RecurrenceType Type { get; private set; }
        public int Interval { get; private set; }
        public DateTime? EndDate { get; private set; }
        public int? OccurrenceCount { get; private set; }
        public DayOfWeek[]? DaysOfWeek { get; private set; }
        public int? DayOfMonth { get; private set; }
        public int? MonthOfYear { get; private set; }
        #endregion

        #region Constructors
        private RecurrencePattern() { } // For EF Core
        #endregion

        #region Factory Methods
        public static RecurrencePattern Create(
            RecurrenceType type,
            int interval,
            DateTime? endDate = null,
            int? occurrenceCount = null,
            DayOfWeek[]? daysOfWeek = null,
            int? dayOfMonth = null,
            int? monthOfYear = null)
        {
            ValidateParameters(type, interval, endDate, occurrenceCount, daysOfWeek, dayOfMonth, monthOfYear);

            return new RecurrencePattern
            {
                Type = type,
                Interval = interval,
                EndDate = endDate,
                OccurrenceCount = occurrenceCount,
                DaysOfWeek = daysOfWeek,
                DayOfMonth = dayOfMonth,
                MonthOfYear = monthOfYear
            };
        }
        #endregion

        #region Public Methods
        public bool IsValid()
        {
            try
            {
                ValidateParameters(Type, Interval, EndDate, OccurrenceCount, DaysOfWeek, DayOfMonth, MonthOfYear);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DateTime CalculateNextDueDate(DateTime currentDueDate)
        {
            if (!IsValid())
                throw new InvalidOperationException("Cannot calculate next due date for invalid recurrence pattern");

            // Ensure we're working with the start of the day
            currentDueDate = currentDueDate.Date;

            return Type switch
            {
                RecurrenceType.Daily => CalculateNextDailyDate(currentDueDate),
                RecurrenceType.Weekly => CalculateNextWeeklyDate(currentDueDate),
                RecurrenceType.Monthly => CalculateNextMonthlyDate(currentDueDate),
                RecurrenceType.Yearly => CalculateNextYearlyDate(currentDueDate),
                RecurrenceType.Custom => CalculateNextCustomDate(currentDueDate),
                _ => throw new ArgumentException($"Unsupported recurrence type: {Type}")
            };
        }

        public override string ToString()
        {
            var parts = new List<string>();

            parts.Add(GetRecurrenceDescription());

            if (EndDate.HasValue)
                parts.Add($"until {EndDate.Value:yyyy-MM-dd}");
            if (OccurrenceCount.HasValue)
                parts.Add($"for {OccurrenceCount} occurrences");

            return string.Join(" ", parts);
        }
        #endregion

        #region Private Methods
        private static void ValidateParameters(
            RecurrenceType type,
            int interval,
            DateTime? endDate,
            int? occurrenceCount,
            DayOfWeek[]? daysOfWeek,
            int? dayOfMonth,
            int? monthOfYear)
        {
            if (interval <= 0)
                throw new ArgumentException("Interval must be greater than 0", nameof(interval));

            if (endDate.HasValue && occurrenceCount.HasValue)
                throw new ArgumentException("Cannot specify both EndDate and OccurrenceCount");

            switch (type)
            {
                case RecurrenceType.Daily:
                    ValidateDailyParameters(daysOfWeek, dayOfMonth);
                    break;

                case RecurrenceType.Weekly:
                    ValidateWeeklyParameters(daysOfWeek, dayOfMonth);
                    break;

                case RecurrenceType.Monthly:
                case RecurrenceType.Yearly:
                    ValidateMonthlyYearlyParameters(daysOfWeek, dayOfMonth, monthOfYear);
                    break;

                case RecurrenceType.Custom:
                    ValidateCustomParameters(daysOfWeek, dayOfMonth);
                    break;

                default:
                    throw new ArgumentException($"Unsupported recurrence type: {type}");
            }
        }

        private static void ValidateDailyParameters(DayOfWeek[]? daysOfWeek, int? dayOfMonth)
        {
            if (daysOfWeek != null || dayOfMonth.HasValue)
                throw new ArgumentException("Daily recurrence should not specify days of week or day of month");
        }

        private static void ValidateWeeklyParameters(DayOfWeek[]? daysOfWeek, int? dayOfMonth)
        {
            if (daysOfWeek == null || !daysOfWeek.Any())
                throw new ArgumentException("Weekly recurrence requires at least one day of week");
            if (dayOfMonth.HasValue)
                throw new ArgumentException("Weekly recurrence should not specify day of month");
        }

        private static void ValidateMonthlyYearlyParameters(DayOfWeek[]? daysOfWeek, int? dayOfMonth, int? monthOfYear)
        {
            if (!dayOfMonth.HasValue || dayOfMonth.Value < 1 || dayOfMonth.Value > 31)
                throw new ArgumentException("Monthly/Yearly recurrence requires valid day of month (1-31)");
            if (monthOfYear.HasValue && (monthOfYear.Value < 1 || monthOfYear.Value > 12))
                throw new ArgumentException("Monthly/Yearly recurrence requires valid month of year (1-12)");
            if (daysOfWeek != null)
                throw new ArgumentException("Monthly/Yearly recurrence should not specify days of week");
        }

        private static void ValidateCustomParameters(DayOfWeek[]? daysOfWeek, int? dayOfMonth)
        {
            if (daysOfWeek != null && !daysOfWeek.Any())
                throw new ArgumentException("If days of week are specified, at least one must be provided");
            if (dayOfMonth.HasValue && (dayOfMonth.Value < 1 || dayOfMonth.Value > 31))
                throw new ArgumentException("Day of month must be between 1 and 31");
        }

        private DateTime CalculateNextDailyDate(DateTime currentDate)
        {
            return currentDate.AddDays(Interval);
        }

        private DateTime CalculateNextWeeklyDate(DateTime currentDate)
        {
            var nextDate = currentDate;
            var daysToAdd = 1;

            // Try to find the next occurrence within the next 7 days
            while (daysToAdd <= 7)
            {
                nextDate = currentDate.AddDays(daysToAdd);
                if (DaysOfWeek!.Contains(nextDate.DayOfWeek))
                {
                    // If we found a valid day, add the interval in weeks
                    return nextDate.AddDays((Interval - 1) * 7);
                }
                daysToAdd++;
            }

            throw new InvalidOperationException("Could not calculate next weekly date");
        }

        private DateTime CalculateNextMonthlyDate(DateTime currentDate)
        {
            var nextDate = currentDate.AddMonths(Interval);
            var daysInMonth = DateTime.DaysInMonth(nextDate.Year, nextDate.Month);
            var targetDay = Math.Min(DayOfMonth!.Value, daysInMonth);

            return new DateTime(nextDate.Year, nextDate.Month, targetDay);
        }

        private DateTime CalculateNextYearlyDate(DateTime currentDate)
        {
            var nextYear = currentDate.Year + Interval;
            var month = MonthOfYear!.Value;
            var day = Math.Min(DayOfMonth!.Value, DateTime.DaysInMonth(nextYear, month));
            return new DateTime(nextYear, month, day);
        }

        private DateTime CalculateNextCustomDate(DateTime currentDate)
        {
            return currentDate.AddDays(Interval);
        }

        private string GetRecurrenceDescription()
        {
            return Type switch
            {
                RecurrenceType.Daily => $"Every {Interval} day(s)",
                RecurrenceType.Weekly => $"Every {Interval} week(s) on {string.Join(", ", DaysOfWeek!.Select(d => d.ToString()))}",
                RecurrenceType.Monthly => $"Every {Interval} month(s) on day {DayOfMonth}",
                RecurrenceType.Yearly => $"Every {Interval} year(s) on {MonthOfYear}/{DayOfMonth}",
                RecurrenceType.Custom => GetCustomDescription(),
                _ => throw new ArgumentException($"Unsupported recurrence type: {Type}")
            };
        }

        private string GetCustomDescription()
        {
            var parts = new List<string> { $"Custom: every {Interval} days" };

            if (DaysOfWeek != null)
                parts.Add($"on {string.Join(", ", DaysOfWeek)}");
            if (DayOfMonth.HasValue)
                parts.Add($"on day {DayOfMonth}");

            return string.Join(" ", parts);
        }
        #endregion
    }
}