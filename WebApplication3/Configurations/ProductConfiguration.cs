using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Model;

namespace WebApplication3.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.ReviewCount).IsRequired();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(256);
            builder.Property(x=>x.Description).IsRequired().HasMaxLength(256);

            builder.HasOne(x=>x.Category).WithMany(x=>x.Products).HasForeignKey(x=>x.CategoryId).OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Price).HasPrecision(7, 2);

            builder.ToTable(
                option =>
                {
                    option.HasCheckConstraint("CK_Product_Rating","[Rating] between 0 and 5");
                    option.HasCheckConstraint("CK_Product_Price","[Price] >= 0 ");
                }
                );

        }
    }
}
