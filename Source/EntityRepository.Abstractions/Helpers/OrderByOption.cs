using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EntityRepository.Abstractions.Helpers
{
    /// <summary>
    /// Class wich implements the return orderby
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class OrderByOption<TEntity>
    {
        /// <summary>
        /// Orders
        /// </summary>
        public List<(Expression<Func<TEntity, object>> OrderProperty, OrderByType OrderType)> Orders { get; set; }
        /// <summary>
        /// Constructor OrderByOption
        /// </summary>
        public OrderByOption()
        {
            Orders = new List<(Expression<Func<TEntity, object>>, OrderByType)>();
        }
        /// <summary>
        /// Add order
        /// </summary>
        /// <param name="orderProperty"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public OrderByOption<TEntity> AddOrder(Expression<Func<TEntity, object>> orderProperty, OrderByType orderType)
        {
            if (orderProperty != null)
                Orders.Add((orderProperty, orderType));
            return this;
        }
    }

    /// <summary>
    /// Orderby types
    /// </summary>
    public enum OrderByType
    {
        Ascending,
        Descending
    }
}
