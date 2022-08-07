using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBook.Core.ProjectAggregate;

namespace TravelBook.Infrastructure.Configurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Ignore(b => b.DomainEvents);

        builder
            .HasOne(a => a.Travel)
            .WithMany(t => t.Articles)
            .HasForeignKey(a => a.TravelId);
    }
}
