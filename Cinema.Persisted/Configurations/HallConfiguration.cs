using Cinema.Persisted.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Persisted.Configurations
{
    public class HallConfiguration : IEntityTypeConfiguration<Hall>
    {
        public void Configure(EntityTypeBuilder<Hall> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.HasQueryFilter(_ => !_.IsDeleted);

            builder
                .HasMany(_ => _.Places)
                .WithOne(_ => _.Hall)
                .HasForeignKey(_ => _.HallId)
                .IsRequired();
        }
    }
}
