using DHSOnlineStore.Data;
using DHSOnlineStore.Models;
using DHSOnlineStore.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DHSOnlineStore.Repositories.Class
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts(string sTerm = "")
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Product> products = await (from product in _context.Products where 
                                                   string.IsNullOrWhiteSpace(sTerm) || 
                                                   product.Name.ToLower().StartsWith(sTerm) select new Product
                                                   {
                                                       Id = product.Id,
                                                       Name = product.Name,
                                                       Brand = product.Brand,
                                                       Image = product.Image,
                                                       Price = product.Price,
                                                       Description = product.Description,
                                                   }).ToListAsync();

            return products;
        }
    }
}
