using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CabFlow.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CabFlow.Data.Configuration
{
    public class PointConfig : IEntityTypeConfiguration<Point>
    {
        public void Configure(EntityTypeBuilder<Point> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Street).IsRequired(false);
            builder.Property(x => x.Number).IsRequired(false);
            builder.Property(x => x.Name).IsRequired(false);
            builder.Property(x => x.City).IsRequired(false);
        }
    }
}
