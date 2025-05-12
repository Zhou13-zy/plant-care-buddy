using System;
using System.Linq;
using System.Linq.Expressions;
using PlantCareBuddy.Domain.Entities;
using PlantCareBuddy.Domain.Enums;

namespace PlantCareBuddy.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<Plant> WhereNameContainsInsensitive(this IQueryable<Plant> query, string value)
        {
            if (string.IsNullOrEmpty(value))
                return query;
            return query.Where(p => p.Name.ToLower().Contains(value.ToLower()));
        }

        public static IQueryable<Plant> WhereSpeciesContainsInsensitive(this IQueryable<Plant> query, string value)
        {
            if (string.IsNullOrEmpty(value))
                return query;
            return query.Where(p => p.Species.ToLower().Contains(value.ToLower()));
        }

        public static IQueryable<Plant> WhereLocationContainsInsensitive(this IQueryable<Plant> query, string value)
        {
            if (string.IsNullOrEmpty(value))
                return query;
            return query.Where(p => p.Location.ToLower().Contains(value.ToLower()));
        }

        // Health status filter (not a string)
        public static IQueryable<Plant> WithHealthStatus(
            this IQueryable<Plant> query,
            PlantHealthStatus? status)
        {
            if (!status.HasValue)
                return query;
            return query.Where(p => p.HealthStatus == status.Value);
        }
        /// <summary>
        /// Returns plants that need attention based on either poor health status or recent acquisition
        /// </summary>
        /// <param name="days">Number of days to consider for recently acquired plants</param>
        public static IQueryable<Plant> NeedingCareWithin(this IQueryable<Plant> query, int days)
        {
            var cutoffDate = DateTime.Now.AddDays(-days);
            return query.Where(p => p.HealthStatus != PlantHealthStatus.Healthy ||
                                  (p.AcquisitionDate >= cutoffDate && p.AcquisitionDate <= DateTime.Now));
        }
        /// <summary>
        /// Filters plants that were acquired within a specific date range
        /// </summary>
        /// <param name="startDate">The start date of the range (inclusive)</param>
        /// <param name="endDate">The end date of the range (inclusive)</param>
        public static IQueryable<Plant> AcquiredBetween(this IQueryable<Plant> query, DateTime startDate, DateTime endDate)
        {
            return query.Where(p => p.AcquisitionDate >= startDate && p.AcquisitionDate <= endDate);
        }
        /// <summary>
        /// Returns plants located in an exact location (not partial match)
        /// </summary>
        /// <param name="location">The exact location to filter by</param>
        public static IQueryable<Plant> ByLocation(this IQueryable<Plant> query, string location)
        {
            if (string.IsNullOrEmpty(location))
                return query;
            return query.Where(p => p.Location == location);
        }
        /// <summary>
        /// Returns plants that were acquired within the specified number of days from now
        /// </summary>
        /// <param name="days">Number of days to look back</param>
        public static IQueryable<Plant> RecentlyAcquired(this IQueryable<Plant> query, int days)
        {
            var cutoffDate = DateTime.Now.AddDays(-days);
            return query.Where(p => p.AcquisitionDate >= cutoffDate);
        }
        /// <summary>
        /// Orders plants by acquisition date with newest plants first
        /// </summary>
        public static IQueryable<Plant> OrderByAcquisitionDateDescending(this IQueryable<Plant> query)
        {
            return query.OrderByDescending(p => p.AcquisitionDate);
        }
        /// <summary>
        /// Returns only plants that have notes associated with them
        /// </summary>
        public static IQueryable<Plant> WithNotes(this IQueryable<Plant> query)
        {
            return query.Where(p => !string.IsNullOrEmpty(p.Notes));
        }
    }
}