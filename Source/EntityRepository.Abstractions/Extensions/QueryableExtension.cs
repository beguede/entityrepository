using EntityRepository.Abstractions.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EntityRepository.Abstractions.Extensions
{
    /// <summary>
    /// Queryable Extension
    /// </summary>
    public static class QueryableExtension
    {
        /// <summary>
        /// Apply Includes Entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> ApplyIncludes<TEntity>(this IQueryable<TEntity> query, Func<IncludeEntity<TEntity>, IncludeEntity<TEntity>> includes)
            where TEntity : class
        {
            var includeEntity = new IncludeEntity<TEntity>();
            if (includes != null)
                includes(includeEntity);
            if (includes != null)
            {
                if (includeEntity.Includes.Any())
                {
                    foreach (var include in includeEntity.Includes)
                    {
                        query = query.Include(include);
                    }
                }
                if (includeEntity.IncludesThenIncludes.Any())
                {
                    foreach (var include in includeEntity.IncludesThenIncludes)
                    {
                        var includableQuery = query.Include(include.Includes);
                        foreach (var thenInclude in include.ThenIncludes)
                        {
                            query = includableQuery.ThenInclude(thenInclude);
                        }
                    }
                }
            }
            return query;
        }
        /// <summary>
        /// Apply Filter Entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> ApplyFilter<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> condition)
        {
            if (condition != null)
                query = query.Where(condition);
            return query;
        }
        /// <summary>
        /// Apply Select Entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> ApplySelect<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, TEntity>> select)
        {
            if (select != null)
                query = query.Select(select);
            return query;
        }
        /// <summary>
        /// Apply Order Entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> ApplyOrder<TEntity>(this IQueryable<TEntity> query, Func<OrderByOption<TEntity>, OrderByOption<TEntity>> orderBy)
            where TEntity : class
        {
            if (orderBy != null)
            {
                var orderByOption = new OrderByOption<TEntity>();
                orderBy(orderByOption);
                foreach (var rule in orderByOption.Orders)
                {
                    if (rule.OrderType == OrderByType.Ascending)
                        query = query.OrderBy(rule.OrderProperty);
                    else
                        query = query.OrderByDescending(rule.OrderProperty);
                }
            }
            return query;
        }
        /// <summary>
        /// Apply Pagination Entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> ApplyPagination<TEntity>(this IQueryable<TEntity> query, int page, int pageSize)
            where TEntity : class
        {
            if (page != 0 && pageSize != 0)
            {
                var skip = ((page + 1) * pageSize) - pageSize;
                query = query.Skip(skip).Take(pageSize);
            }
            return query;
        }
    }
}
