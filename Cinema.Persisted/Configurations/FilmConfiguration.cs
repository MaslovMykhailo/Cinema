using Cinema.Persisted.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Persisted.Configurations
{
    public class FilmConfiguration : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.HasQueryFilter(_ => !_.IsDeleted);

            builder
                .HasMany(_ => _.Tickets)
                .WithOne(_ => _.Film)
                .HasForeignKey(_ => _.FilmId)
                .IsRequired();
        }
    }

}
