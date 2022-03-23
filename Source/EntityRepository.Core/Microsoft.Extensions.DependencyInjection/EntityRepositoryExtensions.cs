using EntityRepository.Abstractions;
using EntityRepository.Core;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class EntityRepositoryExtensions
    {
        public static void AddEntityRepository(this IServiceCollection services)
        {
            services.AddScoped<IReadRepository, ReadRepository>();
            services.AddScoped<IWriteRepository, WriteRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
