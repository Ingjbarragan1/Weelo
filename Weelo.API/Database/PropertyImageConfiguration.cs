using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weelo.API.Models;

namespace Weelo.API.Database
{
    public class PropertyImageConfiguration : IEntityTypeConfiguration<PropertyImage>
    {
        public void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            builder.HasKey(s => s.IdPropertyImage);
            builder.Property(s => s.File)
                .IsRequired();
            builder.Property(s => s.Enable)
                .IsRequired();
        }
    }
}
