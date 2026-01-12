using WebApplication3.Model.Common;

namespace WebApplication3.Model
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; }      
    }
}
