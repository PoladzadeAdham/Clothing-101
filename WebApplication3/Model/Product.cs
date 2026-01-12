using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Model.Common;

namespace WebApplication3.Model
{
    public class Product : BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [Precision(7, 2)]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(0,5)]
        public double Rating { get; set; }
        [Required]
        public int ReviewCount { get; set; }
        [Required]  
        public string ImagePath { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}

