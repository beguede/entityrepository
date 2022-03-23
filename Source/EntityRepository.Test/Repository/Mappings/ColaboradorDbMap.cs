using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace EntityRepository.Test.Repository.Mappings
{
    [ExcludeFromCodeCoverage]
    public class ColaboradorDbMap : IEntityTypeConfiguration<Colaborador>
    {
        public void Configure(EntityTypeBuilder<Colaborador> builder)
        {
            builder.HasKey(c => c.Id)
                .HasName($"pk_colaborador");

            builder.Property(c => c.Id)
                .IsRequired()
                .HasColumnName($"id_colaborador");

            builder.HasOne(x => x.Cargo)
                .WithMany()
                .HasForeignKey(c => c.CargoId)
                .HasConstraintName("fk_colaborador_cargo");

            builder.Property(c => c.CargoId)
                .IsRequired()
                .HasColumnName($"id_cargo");

            builder.Property(p => p.Matricula)
                .IsRequired()
                .HasColumnName("cd_matricula");

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnName("nom_colaborador");

            builder.Property(p => p.Apelido)
                .HasColumnName("nom_apelido");

            builder.Property(p => p.Cpf)
                .HasColumnName("num_cpf");

            builder.Property(p => p.Email)
                .HasColumnName("nom_email");

            builder.Property(p => p.EmailPessoal)
                .HasColumnName("nom_email_pessoal");

            builder.Property(p => p.DataNascimento)
                .HasColumnName("dt_nascimento");

            builder.Property(p => p.Situacao)
                .HasColumnName("sigla_situacao");
        }
    }
}