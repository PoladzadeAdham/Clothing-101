using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Context;
using WebApplication3.ViewModel.ProductViewModel;

namespace WebApplication3.Controllers
{
    public class HomeController(AppDbContext context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var products = await context.Products.Select(p => new ProductGetVm
            {
                Title = p.Title,
                Price = p.Price,
                Description = p.Description,
                Rating = p.Rating,
                ReviewCount = p.ReviewCount,
                ImagePath = p.ImagePath
            })
                .ToListAsync();

            return View(products);
        }
    }
}
