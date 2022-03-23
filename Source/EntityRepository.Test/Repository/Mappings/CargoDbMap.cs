using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityRepository.Test.Repository.Mappings
{
    public class CargoDbMap : IEntityTypeConfiguration<Cargo>
    {
        public void Configure(EntityTypeBuilder<Cargo> builder)
        {
            builder.ToTable("cargo");

            builder.HasKey(c => c.Id)
                .HasName($"pk_cargo");

            builder.Property(c => c.Id)
                .IsRequired()
                .HasColumnName($"id_cargo");

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnName("nom_cargo");
        }
    }
}
