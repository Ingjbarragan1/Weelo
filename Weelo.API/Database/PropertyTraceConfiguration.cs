using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weelo.API.Models;

namespace Weelo.API.Database
{
    public class PropertyTraceConfiguration : IEntityTypeConfiguration<PropertyTrace>
    {
        public void Configure(EntityTypeBuilder<PropertyTrace> builder)
        {
            builder.HasKey(s => s.IdPropertyTrace);
            builder.Property(s => s.Name)
                .IsRequired();
            builder.Property(s => s.Tax)
                .IsRequired();
            builder.Property(s => s.Value)
                .IsRequired();
            builder.Property(s => s.DateSale)
                .IsRequired();
        }
    }
}
