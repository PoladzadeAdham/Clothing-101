using System.ComponentModel.DataAnnotations;

namespace WebApplication3.ViewModel.ProductViewModel
{
    public class ProductCreateVm
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(0,5)]
        public double Rating { get; set; }
        [Range(0, int.MaxValue)]
        public int ReviewCount { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }
}
