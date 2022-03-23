using EntityRepository.Abstractions.Helpers;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EntityRepository.Abstractions
{
    public interface IReadRepository
    {
        /// <summary>
        /// FindById
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="id">Id</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        Task<TEntity> FindById<TEntity>(Guid id, CancellationToken token = default)
            where TEntity : class;
        /// <summary>
        /// FindByFilter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="select"></param>
        /// <param name="orders"></param>
        /// <param name="joins"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<TEntity> FindByFilter<TEntity>(Expression<Func<TEntity, bool>> filter,
            Func<OrderByOption<TEntity>, OrderByOption<TEntity>>? orders = null,
            Func<IncludeEntity<TEntity>, IncludeEntity<TEntity>>? joins = null,
            Expression<Func<TEntity, TEntity>>? select = null,
            CancellationToken token = default)
            where TEntity : class;
        /// <summary>
        /// Count
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="token">Token</param>
        /// <returns></returns>
        Task<long> Count<TEntity>(CancellationToken token = default)
            where TEntity : class;
        /// <summary>
        /// Count
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="filter">Filter</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        Task<long> Count<TEntity>(Expression<Func<TEntity, bool>> filter, CancellationToken token = default)
            where TEntity : class;
        /// <summary>
        /// GetByFilter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="orders"></param>
        /// <param name="joins"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetByFilter<TEntity>(Expression<Func<TEntity, bool>> filter,
            Func<OrderByOption<TEntity>, OrderByOption<TEntity>>? orders = null,
            Func<IncludeEntity<TEntity>, IncludeEntity<TEntity>>? joins = null,
            CancellationToken token = default)
            where TEntity : class;
        /// <summary>
        /// GetByFilter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="select"></param>
        /// <param name="orders"></param>
        /// <param name="joins"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetByFilter<TEntity>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TEntity>>? select,
            Func<OrderByOption<TEntity>, OrderByOption<TEntity>>? orders = null,
            Func<IncludeEntity<TEntity>, IncludeEntity<TEntity>>? joins = null,
            CancellationToken token = default)
            where TEntity : class;
        /// <summary>
        /// GetPagedQuery
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="args">Args</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        Task<PagedQueryResult<TEntity>> GetPagedQuery<TEntity>(PagedQueryArgs<TEntity> args, CancellationToken token = default)
            where TEntity : class;
        /// <summary>
        /// Run Sql
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<TEntity>> RunSql<TEntity>(string sql, object param, CancellationToken token = default)
            where TEntity : class;
        /// <summary>
        /// RunSqlKata
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IEnumerable<TModel>> RunSqlKata<TModel>(Query query);
    }
}