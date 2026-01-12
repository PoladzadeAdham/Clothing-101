using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Model;

namespace WebApplication3.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);

            builder.HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);

        }
    }
}
