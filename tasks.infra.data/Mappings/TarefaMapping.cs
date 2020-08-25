using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tasks.domain.Entities;

namespace tasks.infra.data.Mappings
{
    public class TarefaMapping : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Status)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Estimado)
                .IsRequired()
                .HasColumnType("DateTime");

            builder.Property(c => c.Concluido)
                .HasColumnType("DateTime");
        }
    }
}