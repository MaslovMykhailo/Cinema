using Cinema.Persisted.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Persisted.Configurations
{
    public class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.HasQueryFilter(_ => !_.IsDeleted);

            builder
                .HasOne(_ => _.Hall)
                .WithMany(_ => _.Places)
                .HasForeignKey(_ => _.HallId)
                .IsRequired();

            builder
                .HasOne(_ => _.Ticket)
                .WithOne(_ => _.Place)
                .HasForeignKey<Place>(_ => _.TicketId);
        }
    }
}
