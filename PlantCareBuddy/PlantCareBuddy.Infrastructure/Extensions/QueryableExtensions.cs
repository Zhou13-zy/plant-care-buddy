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
    }
}