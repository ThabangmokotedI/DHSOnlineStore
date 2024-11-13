using DHSOnlineStore.Data;
using DHSOnlineStore.ImageService;
using DHSOnlineStore.Models;
using DHSOnlineStore.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DHSOnlineStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _repository;
        private readonly IFileService _fileService;

        public ProductController(IProductRepository repository, IFileService fileService, ApplicationDbContext context)
        {
            _repository = repository;
            _fileService = fileService;
            _context = context;
        }

        public async Task<IActionResult> Products(string sTerm = "")
        {
            var products = await _repository.GetProducts(sTerm);

            return View(products);
        }

        public async Task<IActionResult> Index()
        {
            var products = await _repository.GetProducts();

            return View(products);
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return View(product);
        }

        public IActionResult AddProduct()
        {
            Product product = new Product();
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (ModelState.IsValid)
                return View(product);

            try
            {
                if (product.ImageFile != null)
                {
                    string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                    string imageName = await _fileService.SaveFile(product.ImageFile, allowedExtensions);
                    product.Image = imageName;
                }
                Product prod = new Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Brand = product.Brand,
                    Image = product.Image,
                    Price = product.Price,
                    Description = product.Description,
                };

                await _context.Products.AddAsync(prod);
                await _context.SaveChangesAsync();
                TempData["successMessage"] = "Product added successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Product not added succesfully!";
                Console.WriteLine(ex.ToString());
                return View(product);
            }
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            if (ModelState.IsValid)
                return View(product);

            try
            {
                string oldImage = "";
                if (product.ImageFile != null)
                {
                    string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                    string imageName = await _fileService.SaveFile(product.ImageFile, allowedExtensions);
                    oldImage = product.Image;
                    product.Image = imageName;
                }
                Product prod = new Product()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Brand = product.Brand,
                    Image = product.Image,
                    Price = product.Price,
                    Description = product.Description,
                };

                _context.Products.Update(prod);
                await _context.SaveChangesAsync();
                if (!string.IsNullOrWhiteSpace(oldImage))
                {
                    _fileService.DeleteFile(oldImage);
                }
                TempData["successMessage"] = "Product updated successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Product not updated succesfully!";
                Console.WriteLine(ex.ToString());
                return View(product);
            }
        }
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var product = await _context.Products.FindAsync(id);
			return View(product);
		}
		[HttpPost]
        public async Task<IActionResult> DeleteProduct(Product product)
        {
            try
            {
                //var product = await _context.Products.FindAsync(id);
                //if (product == null)
                //{
                //    TempData["errorMessage"] = $"Product with id: {id} was not found";
                //    return View(product);
                //}
                //else
                //{
                //    _context.Products.Remove(product);
                //    await _context.SaveChangesAsync();
                //    if (!string.IsNullOrWhiteSpace(product.Image))
                //    {
                //        _fileService.DeleteFile(product.Image);
                //    }
                //    TempData["successMessage"] = "Product successfully deleted";
                //    return RedirectToAction("Index");

                //}

				_context.Products.Remove(product);
				await _context.SaveChangesAsync();
				if (!string.IsNullOrWhiteSpace(product.Image))
				{
					_fileService.DeleteFile(product.Image);
				}
				TempData["successMessage"] = "Product successfully deleted";
				return RedirectToAction("Index");
			}
            catch (Exception ex)
            {

                TempData["errorMessage"] = "Product not deleted succesfully!";
                Console.WriteLine(ex.ToString());
                return View("Index");
            }
        }
    }
}
