using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weelo.API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Weelo.API.Database
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(s => s.IdProperty);
            builder.Property(s => s.Name)
                .IsRequired();
            builder.Property(s => s.Address)
                .IsRequired();
            builder.Property(s => s.Price)
                .IsRequired();
            builder.Property(s => s.CodeInternal)
                .IsRequired();
            builder.Property(s => s.Year)
                .IsRequired();
        }
    }
}
