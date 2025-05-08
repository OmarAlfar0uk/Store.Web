
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configrations
{
    internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {

            builder.ToTable("DeliveryMethod");
            builder.Property(P => P.Price)
                .HasColumnType("decimal(8,2)");


            builder.Property(P => P.ShortName)
                .HasColumnType("Varchar")
                .HasMaxLength(50);
            
            builder.Property(P => P.Description)
                .HasColumnType("Varchar")
                .HasMaxLength(100);
            
            builder.Property(P => P.DeliveryTime)
                .HasColumnType("Varchar")
                .HasMaxLength(50);



        }
    }
}
