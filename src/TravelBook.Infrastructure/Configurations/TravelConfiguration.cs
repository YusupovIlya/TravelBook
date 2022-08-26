using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBook.Core.ProjectAggregate;

namespace TravelBook.Infrastructure.Configurations;

public class TravelConfiguration : IEntityTypeConfiguration<Travel>
{
    public void Configure(EntityTypeBuilder<Travel> builder)
    {
        builder.HasKey(o => o.Id);

        builder
            .Property(t => t.DateStartTravel)
            .HasColumnType("date");

        builder
            .Property(t => t.DateFinishTravel)
            .HasColumnType("date");

        builder.OwnsOne(o => o.Place);

        builder
            .HasOne<IdentityUser>()
            .WithMany()
            .HasForeignKey(t => t.UserId);

    }
}

