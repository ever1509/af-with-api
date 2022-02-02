using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations;
public class ClickConfiguration : IEntityTypeConfiguration<Click>
{
    public void Configure(EntityTypeBuilder<Click> builder)
    {
        builder.HasKey(x => x.ClickId);

        builder.HasOne(x => x.Url)
            .WithMany(e => e.Clicks)
            .HasForeignKey(x => x.UrlId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Url_Click");
    }
}

