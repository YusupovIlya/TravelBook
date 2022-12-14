using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBook.Core.ProjectAggregate;

namespace TravelBook.Infrastructure.Configurations;

public class PhotoAlbumConfiguration : IEntityTypeConfiguration<PhotoAlbum>
{
    public void Configure(EntityTypeBuilder<PhotoAlbum> builder)
    {
        builder.HasKey(o => o.Id);

        builder
            .HasOne(pa => pa.Travel)
            .WithMany(t => t.PhotoAlbums)
            .HasForeignKey(pa => pa.TravelId);
    }
}
