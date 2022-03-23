using EntityRepository.Abstractions;
using Microsoft.EntityFrameworkCore;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace EntityRepository.Core
{
    /// <summary>
    /// Write Repository
    /// </summary>
    public class WriteRepository : IWriteRepository, IDisposable
    {
        private bool disposedValue;
        private readonly DbContext context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public WriteRepository(DbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public async ValueTask<TEntity> Insert<TEntity>(TEntity entity, CancellationToken token = default)
            where TEntity : class
        {
            var savedEntity = await context.AddAsync(entity, token);

            return savedEntity.Entity;
        }

        /// <summary>
        /// InsertMultiple
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entities">Entities</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public async ValueTask<IEnumerable<TEntity>> InsertMultiple<TEntity>(IEnumerable<TEntity> entities, [EnumeratorCancellation] CancellationToken token = default)
            where TEntity : class
        {
            var savedEntities = new List<TEntity>();

            foreach (var entity in entities)
            {
                var savedEntity = await context.AddAsync(entity);
                savedEntities.Add(savedEntity.Entity);
            }

            return savedEntities;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <typeparam name="TEntity">TEntitytypeparam>
        /// <param name="entity">Entity</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public async ValueTask<TEntity> Update<TEntity>(TEntity entity, CancellationToken token = default)
            where TEntity : class
        {
            var updated = context.Set<TEntity>().Update(entity);
            context.Entry(entity).State = EntityState.Modified;

            return await Task.FromResult(updated.Entity);
        }

        /// <summary>
        /// Update with filter
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="filterToUpdate">FilterToUpdate</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public ValueTask<TEntity> Update<TEntity>(TEntity entity, Expression<Func<TEntity, bool>> filterToUpdate, CancellationToken token = default)
            where TEntity : class
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// UpdateOnly
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="fieldsToUpdate">FieldsToUpdate</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public ValueTask<TEntity> UpdateOnly<TEntity>(TEntity entity, Expression<Func<TEntity, object>> fieldsToUpdate, CancellationToken token)
            where TEntity : class
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public async ValueTask<bool> Delete<TEntity>(TEntity entity, CancellationToken token = default)
            where TEntity : class
        {
            var db = context.Set<TEntity>();
            db.Remove(entity);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// DeleteRange
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entities">Entities</param>
        /// <returns></returns>
        public async ValueTask<bool> DeleteRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            var db = context.Set<TEntity>();
            db.RemoveRange(entities);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// ExecuteSqlKata
        /// </summary>
        /// <param name="query">Query</param>
        /// <returns></returns>
        public async ValueTask<int> ExecuteSqlKata(Query query)
        {
            var db = new QueryFactory(context.Database.GetDbConnection(), new MySqlCompiler());

            return await db.ExecuteAsync(query);
        }

        /// <summary>
        /// ExecuteSql
        /// </summary>
        /// <param name="sql">Sql</param>
        /// <param name="param">Param</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public ValueTask<bool> ExecuteSql(string sql, object param, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue)
                return;

            if (disposing)
                context.Dispose();

            disposedValue = true;
        }

        ~WriteRepository() =>
            Dispose(disposing: false);

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
