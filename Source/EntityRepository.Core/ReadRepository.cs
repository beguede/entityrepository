using EntityRepository.Abstractions;
using EntityRepository.Abstractions.Extensions;
using EntityRepository.Abstractions.Helpers;
using Microsoft.EntityFrameworkCore;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EntityRepository.Core
{
    /// <summary>
    /// Read Repository
    /// </summary>
    public class ReadRepository : IReadRepository, IDisposable
    {
        private bool disposedValue;
        private readonly DbContext context;
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        public ReadRepository(DbContext context) =>
            this.context = context;
        /// <summary>
        /// Find By Id 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TEntity> FindById<TEntity>(Guid id, CancellationToken token = default)
            where TEntity : class
        {
            var db = context.Set<TEntity>();
            return await db.FindAsync(new object[] { id }, token);
        }
        /// <summary>
        /// Find By Filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="joins"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TEntity> FindByFilter<TEntity>(Expression<Func<TEntity, bool>> filter,
            Func<OrderByOption<TEntity>, OrderByOption<TEntity>>? orders = null,
            Func<IncludeEntity<TEntity>, IncludeEntity<TEntity>>? joins = null,
            Expression<Func<TEntity, TEntity>>? select = null,
            CancellationToken token = default)
            where TEntity : class
        {
            var db = context.Set<TEntity>();
            var query = db.Where(filter);
            if (orders != null)
                query = query.ApplyOrder(orders);
            if (joins != null)
                query = query.ApplyIncludes(joins);
            if (select != null)
                query = query.ApplySelect(select);
            return await Task.FromResult(query.AsNoTracking().FirstOrDefault());
        }
        /// <summary>
        /// Find By Filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="joins"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TEntity> FindByFilter<TEntity, TResult>(Expression<Func<TEntity, bool>> filter,
            Func<OrderByOption<TEntity>, OrderByOption<TEntity>>? orders = null,
            Func<IncludeEntity<TEntity>, IncludeEntity<TEntity>>? joins = null,
            Expression<Func<TEntity, TEntity>>? select = null,
            CancellationToken token = default)
            where TEntity : class
        {
            var db = context.Set<TEntity>();
            var query = db.Where(filter);
            if (orders != null)
                query = query.ApplyOrder(orders);
            if (joins != null)
                query = query.ApplyIncludes(joins);
            if (select != null)
                query = query.ApplySelect(select);
            return await Task.FromResult(query.AsNoTracking().FirstOrDefault());
        }
        /// <summary>
        /// Count
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<long> Count<TEntity>(CancellationToken token = default)
            where TEntity : class
        {
            var db = context.Set<TEntity>();
            return await db.CountAsync(token);
        }
        /// <summary>
        /// Count Expression
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<long> Count<TEntity>(Expression<Func<TEntity, bool>> filter, CancellationToken token = default)
            where TEntity : class
        {
            var db = context.Set<TEntity>();
            return await db.CountAsync(filter, token);
        }
        /// <summary>
        /// Get By Filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="joins"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByFilter<TEntity>(Expression<Func<TEntity, bool>> filter,
            Func<OrderByOption<TEntity>, OrderByOption<TEntity>>? orders = null,
            Func<IncludeEntity<TEntity>, IncludeEntity<TEntity>>? joins = null,
            CancellationToken token = default)
            where TEntity : class
        {
            var db = context.Set<TEntity>();
            var query = db.Where(filter);
            if (orders != null)
                query = query.ApplyOrder(orders);
            if (joins != null)
                query = query.ApplyIncludes(joins);
            return await query.AsNoTracking().ToListAsync();
        }
        /// <summary>
        /// Get By Filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="joins"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetByFilter<TEntity>(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TEntity>>? select,
            Func<OrderByOption<TEntity>, OrderByOption<TEntity>>? orders = null,
            Func<IncludeEntity<TEntity>, IncludeEntity<TEntity>>? joins = null,
            CancellationToken token = default)
            where TEntity : class
        {
            var db = context.Set<TEntity>();
            var query = db.Where(filter);
            if (select != null)
                query = query.ApplySelect(select);
            if (orders != null)
                query = query.ApplyOrder(orders);
            if (joins != null)
                query = query.ApplyIncludes(joins);
            return await query.AsNoTracking().ToListAsync();
        }
        /// <summary>
        /// Get Paged Query
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="args"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<PagedQueryResult<TEntity>> GetPagedQuery<TEntity>(PagedQueryArgs<TEntity> args, CancellationToken token = default)
            where TEntity : class
        {
            var db = context.Set<TEntity>();
            var query = db.AsQueryable<TEntity>();
            var count = await query.CountAsync(token);
            query = query.ApplyIncludes(args.Joins);
            query = query.ApplyFilter(args.Filter);
            query = query.ApplyOrder(args.OrderBy);
            query = query.ApplyPagination(args.PageNumber, args.PageSize);
            var items = new List<TEntity>();
            if (args.Select != null)
                items = await query.Select(args.Select).ToListAsync();
            else
                items = await query.ToListAsync();
            return new PagedQueryResult<TEntity>(args.PageNumber, args.PageSize, count, items);
        }
        /// <summary>
        /// Run Sql
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<List<TEntity>> RunSql<TEntity>(string sql, object param, CancellationToken token = default)
            where TEntity : class
        {
            var db = context.Set<TEntity>();
            return db.FromSqlRaw(sql, param).ToListAsync(token);
        }
        /// <summary>
        /// Run Sql Kata
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TModel>> RunSqlKata<TModel>(Query query)
        {
            var db = new QueryFactory(context.Database.GetDbConnection(), new MySqlCompiler());
            return (await db.GetAsync<TModel>(query)).ToList();
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
        ~ReadRepository() =>
            Dispose(disposing: false);
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
