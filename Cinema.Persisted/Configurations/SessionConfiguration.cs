using Cinema.Persisted.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Persisted.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.HasQueryFilter(_ => !_.IsDeleted);

            builder
                .HasOne(_ => _.Film)
                .WithMany(_ => _.Sessions)
                .HasForeignKey(_ => _.FilmId)
                .IsRequired();

            builder
                .HasOne(_ => _.Hall)
                .WithMany(_ => _.Sessions)
                .HasForeignKey(_ => _.HallId)
                .IsRequired();
        }
    }
}
