using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EntityRepository.Abstractions
{
    /// <summary>
    /// IUnitOfWork
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// CommitAsync
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();

        /// <summary>
        /// DisposeAsync
        /// </summary>
        /// <returns></returns>
        Task DisposeAsync();
    }
}
