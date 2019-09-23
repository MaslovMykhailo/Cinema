using Cinema.Persisted.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Persisted.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.HasQueryFilter(_ => !_.IsDeleted);

            builder
                .HasOne(_ => _.Film)
                .WithMany(_ => _.Tickets)
                .HasForeignKey(_ => _.FilmId)
                .IsRequired();

            builder
                .HasOne(_ => _.Place)
                .WithOne(_ => _.Ticket)
                .HasForeignKey<Ticket>(_ => _.PlaceId);

            builder
                .HasOne(_ => _.Visitor)
                .WithOne(_ => _.Ticket)
                .HasForeignKey<Ticket>(_ => _.VisitorId);
        }
    }
}
