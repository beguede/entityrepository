using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EntityRepository.Abstractions
{
    /// <summary>
    /// IWriteRepository
    /// </summary>
    public interface IWriteRepository
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        ValueTask<TEntity> Insert<TEntity>(TEntity entity, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// InsertMultiple
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entities">Entities</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        ValueTask<IEnumerable<TEntity>> InsertMultiple<TEntity>(IEnumerable<TEntity> entities, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="TEntity">TEntitytypeparam>
        /// <param name="entity">Entity</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        ValueTask<TEntity> Update<TEntity>(TEntity entity, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="filterToUpdate">FilterToUpdate</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        ValueTask<TEntity> Update<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> filterToUpdate, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// UpdateOnly
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="fieldsToUpdate">FieldsToUpdate</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        ValueTask<TEntity> UpdateOnly<TEntity>(TEntity entity, Expression<Func<TEntity, object>> fieldsToUpdate, CancellationToken token)
            where TEntity : class;

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        ValueTask<bool> Delete<TEntity>(TEntity entity, CancellationToken token = default)
            where TEntity : class;

        /// <summary>
        /// DeleteRange
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entities">Entities</param>
        /// <returns></returns>
        ValueTask<bool> DeleteRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;

        /// <summary>
        /// ExecuteSqlKata
        /// </summary>
        /// <param name="query">Query</param>
        /// <returns></returns>
        ValueTask<int> ExecuteSqlKata(Query query);

        /// <summary>
        /// ExecuteSql
        /// </summary>
        /// <param name="sql">Sql</param>
        /// <param name="param">Param</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        ValueTask<bool> ExecuteSql(string sql, object param, CancellationToken token = default);
    }
}
