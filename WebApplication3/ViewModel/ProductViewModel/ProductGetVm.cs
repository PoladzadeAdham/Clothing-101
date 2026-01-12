using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.ViewModel.ProductViewModel
{
    public class ProductGetVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public int ReviewCount { get; set; }
        public string ImagePath { get; set; }
        public string CategoryName { get; set; }
    }
}
