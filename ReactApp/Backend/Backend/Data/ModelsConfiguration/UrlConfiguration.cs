using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.Models;

namespace Backend.Data.ModelsConfiguration
{
    public class UrlConfiguration : IEntityTypeConfiguration<UrlInfo>
    {
        public void Configure(EntityTypeBuilder<UrlInfo> builder)
        {
            builder
                .Property(el => el.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            builder
                .HasIndex(el => el.UrlString)
                .IsUnique();
        }
    }
}
