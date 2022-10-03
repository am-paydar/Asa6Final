using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ImageEntityConfig : IEntityTypeConfiguration<ImageEntity>
    {
        public void Configure(EntityTypeBuilder<ImageEntity> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Type).HasMaxLength(10).IsRequired();
            builder.Property(a => a.CreatedOn).IsRequired();
            builder.Property(a => a.NormalPath).HasMaxLength(500).IsRequired();
            builder.Property(a => a.BigPath).HasMaxLength(500);
            builder.Property(a => a.TinyPath).HasMaxLength(500);
            builder.Property(a => a.Flag).HasDefaultValue(false);
            builder.Property(a => a.IsRemove).HasDefaultValue(false);
        }
    }
}
