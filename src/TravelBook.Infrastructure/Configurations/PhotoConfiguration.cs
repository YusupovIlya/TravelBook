using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBook.Core.ProjectAggregate;

namespace TravelBook.Infrastructure.Configurations;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.HasKey(o => o.Id);

        builder
            .HasOne(p => p.PhotoAlbum)
            .WithMany(pa => pa.Photos)
            .HasForeignKey(p => p.PhotoAlbumId);
    }
}
