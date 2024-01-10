using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
           
           builder.Property(p=>p.Name).IsRequired().HasMaxLength(100);
           builder.Property(p=>p.Description).HasMaxLength(300).IsRequired();
           builder.Property(p=>p.Price).IsRequired().HasColumnType("decimal(18,2)");
           builder.Property(p=>p.PictureUrl).IsRequired().HasMaxLength(150);
           builder.HasOne(c=>c.Category).WithMany().HasForeignKey(p=>p.CategoryId);
           builder.HasOne(p=>p.Publisher).WithMany().HasForeignKey(a=>a.PublisherId);

        }
    }
}