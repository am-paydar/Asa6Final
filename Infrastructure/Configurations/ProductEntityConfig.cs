using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class ProductEntityConfig : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
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
            builder.HasIndex(a => a.ReceivedID).IsUnique();
        }


    }
}
