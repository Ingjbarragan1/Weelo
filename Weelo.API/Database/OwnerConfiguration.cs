using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weelo.API.Models;

namespace Weelo.API.Database
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasKey(s => s.IdOwner);
            builder.Property(s => s.Name)
                .IsRequired();
            builder.Property(s => s.Address)
                .IsRequired();
            builder.Property(s => s.Photo)
                .IsRequired();
            builder.Property(s => s.Birthday)
                .IsRequired();
        }
    }
}
