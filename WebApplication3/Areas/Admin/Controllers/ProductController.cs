using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication3.Context;
using WebApplication3.Extension;
using WebApplication3.Model;
using WebApplication3.ViewModel.ProductViewModel;

namespace WebApplication3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly string _folderPath;

        public ProductController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _folderPath = Path.Combine(_environment.WebRootPath, "assets", "images");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            await SendCategoriesWithViewBag();

            var product = await _context.Products.Select(x=> new ProductUpdateVm
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                ImagePath = x.ImagePath,
                Rating = x.Rating,
                ReviewCount = x.ReviewCount,
                CategoryId = x.CategoryId,
            })
                .FirstOrDefaultAsync(x=>x.Id == id);

            if (product == null)
            {
                return NotFound();
            }



            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateVm vm)
        {
            await SendCategoriesWithViewBag();

            if (!ModelState.IsValid)
                return View(vm);

            if (!vm.Image?.CheckSize(2) ?? false)
            {
                ModelState.AddModelError("ImageSize", "Image size 2 mb dan az olmalidir");
                return View(vm);
            }

            if (!vm.Image?.Checktype() ?? false)
            {
                ModelState.AddModelError("ImageType", "Image sekil tipinde olmalidir.");
                return View(vm);
            }

            Product? product = await _context.Products.FindAsync(vm.Id); 

            if (product == null) 
                return NotFound();

            product.Rating = vm.Rating;
            product.ReviewCount = vm.ReviewCount;   
            product.CategoryId = vm.CategoryId;
            product.Price = vm.Price;
            product.Description = vm.Description;
            product.Title = vm.Title;


            if (vm.Image is { })
            {
                string filePath = Path.Combine(_folderPath, product.ImagePath);

                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                string uniqueFileName = await vm.Image.SaveFileAsync(_folderPath);
                
                product.ImagePath = uniqueFileName;
            }


            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }



        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            string filePath = Path.Combine(_folderPath, product.ImagePath);

            if(System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
            
            return RedirectToAction("Index");


        }


        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Select(x => new ProductGetVm
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    Price = x.Price,
                    Rating = x.Rating,
                    ReviewCount = x.ReviewCount,
                    CategoryName = x.Category.Name
                })
                .ToListAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await SendCategoriesWithViewBag();

            return View();
        }

        private async Task SendCategoriesWithViewBag()
        {
            var categories = await _context.Categories.ToListAsync();

            ViewBag.Categories = categories;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVm vm)
        {
            await SendCategoriesWithViewBag();

            if (!ModelState.IsValid)
                return View(vm);

            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("ImageSize", "Image size 2 mb dan az olmalidir");
                return View(vm);
            }

            if (!vm.Image.Checktype())
            {
                ModelState.AddModelError("ImageType", "Image sekil tipinde olmalidir.");
                return View(vm);
            }


            string uniqueFileName = await vm.Image.SaveFileAsync(_folderPath);


            Product product = new()
            {
                Title = vm.Title,
                Description = vm.Description,
                Price = vm.Price,
                Rating = vm.Rating,
                ReviewCount = vm.ReviewCount,
                ImagePath = uniqueFileName,
                CategoryId = vm.CategoryId,
            };

            await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

    }
}
