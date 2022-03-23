using EntityRepository.Test.Repository.Mappings;
using Microsoft.EntityFrameworkCore;

namespace EntityRepository.Test.Repository
{
    public class ColaboradorDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(string.Format(EnvironmentVariables.Database.Colaborador.ConnectionString,
                                                        EnvironmentVariables.Database.Colaborador.Database,
                                                        EnvironmentVariables.Database.Colaborador.User,
                                                        EnvironmentVariables.Database.Colaborador.Password));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Colaborador>(new ColaboradorDbMap().Configure);
            modelBuilder.Entity<Cargo>(new CargoDbMap().Configure);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Colaborador> Colaborador { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
    }
}
