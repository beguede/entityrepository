using System;
using System.Linq.Expressions;

namespace EntityRepository.Abstractions.Helpers
{
    /// <summary>
    /// Class wich implements the return paginated queries
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class PagedQueryArgs<TEntity>
    {
        /// <summary>
        /// Page size
        /// </summary>
        public readonly int PageSize = 20;
        /// <summary>
        /// Page number
        /// </summary>
        public readonly int PageNumber = 0;
        /// <summary>
        /// Filter
        /// </summary>
        public Expression<Func<TEntity, bool>>? Filter { get; }
        /// <summary>
        /// Select
        /// </summary>
        public Expression<Func<TEntity, TEntity>>? Select { get; }
        /// <summary>
        /// Joins
        /// </summary>
        public Func<IncludeEntity<TEntity>, IncludeEntity<TEntity>>? Joins { get; }
        /// <summary>
        /// Order by
        /// </summary>
        public Func<OrderByOption<TEntity>, OrderByOption<TEntity>>? OrderBy { get; }
        /// <summary>
        /// Constructor all params
        /// </summary>
        /// <param name="number"></param>
        /// <param name="size"></param>
        /// <param name="select"></param>
        /// <param name="filter"></param>
        /// <param name="joins"></param>
        /// <param name="orders"></param>
        public PagedQueryArgs(int number, int size,
            Expression<Func<TEntity, TEntity>>? select,
            Expression<Func<TEntity, bool>>? filter,
            Func<IncludeEntity<TEntity>, IncludeEntity<TEntity>>? joins,
            Func<OrderByOption<TEntity>, OrderByOption<TEntity>>? orders) =>
            (PageNumber, PageSize, Select, Filter, Joins, OrderBy) = (number, size, select, filter, joins, orders);
        /// <summary>
        /// Constructor number and size
        /// </summary>
        /// <param name="number"></param>
        /// <param name="size"></param>
        public PagedQueryArgs(int number, int size)
            : this(number, size, null, null, null, null) { }
        /// <summary>
        /// Constructor number, size and filter
        /// </summary>
        /// <param name="number"></param>
        /// <param name="size"></param>
        /// <param name="filter"></param>
        public PagedQueryArgs(int number, int size, Expression<Func<TEntity, bool>> filter)
            : this(number, size, null, filter, null, null) { }
        /// <summary>
        /// Constructor number, size, filter and select
        /// </summary>
        /// <param name="number"></param>
        /// <param name="size"></param>
        /// <param name="filter"></param>
        /// <param name="select"></param>
        public PagedQueryArgs(int number, int size, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TEntity>> select)
            : this(number, size, select, filter, null, null) { }
    }

    /// <summary>
    /// Class wich implements the return paginated queries map
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    public class PagedQueryMapArgs<TEntity, TModel> : PagedQueryArgs<TEntity>
    {
        /// <summary>
        /// Select
        /// </summary>
        public new Expression<Func<TEntity, TModel>>? Select { get; }
        /// <summary>
        /// Constructor all params
        /// </summary>
        /// <param name="number"></param>
        /// <param name="size"></param>
        /// <param name="select"></param>
        /// <param name="filter"></param>
        /// <param name="joins"></param>
        /// <param name="orders"></param>
        public PagedQueryMapArgs(int number, int size,
            Expression<Func<TEntity, TModel>>? select,
            Expression<Func<TEntity, bool>>? filter,
            Func<IncludeEntity<TEntity>, IncludeEntity<TEntity>>? joins,
            Func<OrderByOption<TEntity>, OrderByOption<TEntity>>? orders)
            : base(number, size, null, filter, joins, orders) => Select = select;
        /// <summary>
        /// Constructor select with base params
        /// </summary>
        /// <param name="number"></param>
        /// <param name="size"></param>
        /// <param name="select"></param>
        public PagedQueryMapArgs(int number, int size, Expression<Func<TEntity, TModel>> select)
            : base(number, size, null, null, null, null) => Select = select;
        /// <summary>
        /// Constructor select and filter with base params
        /// </summary>
        /// <param name="number"></param>
        /// <param name="size"></param>
        /// <param name="select"></param>
        /// <param name="filter"></param>
        public PagedQueryMapArgs(int number, int size, Expression<Func<TEntity, TModel>> select, Expression<Func<TEntity, bool>> filter)
            : base(number, size, null, filter, null, null) => Select = select;
    }
}
