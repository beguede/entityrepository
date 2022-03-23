using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EntityRepository.Abstractions.Helpers
{
    /// <summary>
    /// Class wich implements the return include entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class IncludeEntity<TEntity>
    {
        /// <summary>
        /// Includes
        /// </summary>
        public List<Expression<Func<TEntity, object>>> Includes { get; private set; }
        /// <summary>
        /// IncludesThenIncludes
        /// </summary>
        public List<(Expression<Func<TEntity, object>> Includes, List<Expression<Func<object, object>>> ThenIncludes)> IncludesThenIncludes { get; private set; }
        /// <summary>
        /// Constructor IncludeEntity
        /// </summary>
        public IncludeEntity()
        {
            Includes = new List<Expression<Func<TEntity, object>>>();
            IncludesThenIncludes = new List<(Expression<Func<TEntity, object>> Includes, List<Expression<Func<object, object>>> ThenIncludes)>();
        }
        /// <summary>
        /// Include
        /// </summary>
        /// <param name="include"></param>
        /// <returns></returns>
        public IncludeEntity<TEntity> Include(Expression<Func<TEntity, object>> include)
        {
            Includes.Add(include);
            return this;
        }
        /// <summary>
        /// IncludeAndThenInclude
        /// </summary>
        /// <param name="include"></param>
        /// <param name="thenInclude"></param>
        /// <returns></returns>
        public IncludeEntity<TEntity> IncludeAndThenInclude(Expression<Func<TEntity, object>> include, params Expression<Func<object, object>>[] thenInclude)
        {
            IncludesThenIncludes.Add((include, thenInclude.ToList()));
            return this;
        }
    }
}
