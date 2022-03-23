using EntityRepository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EntityRepository.Core
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Commit Async
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {
            context.ChangeTracker.DetectChanges();
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Dispose Async
        /// </summary>
        /// <returns></returns>
        public async Task DisposeAsync()
        {
            await Task.Run(() => Dispose());
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
