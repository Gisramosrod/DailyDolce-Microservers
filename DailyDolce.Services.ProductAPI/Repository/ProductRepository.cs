using AutoMapper;
using DailyDolce.Services.ProductApi.Data;
using DailyDolce.Services.ProductApi.Dtos;
using DailyDolce.Services.ProductApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace DailyDolce.Services.ProductApi.Repository {
    public class ProductRepository : IProductRepository {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(DataContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> GetProducts() {
            List<Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);

        }
        public async Task<ProductDto> GetProduct(int id) {
            Product product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto) {
           
            Product product = _mapper.Map<Product>(productDto);

            if(product.Id > 0) _context.Update(product);
            else _context.Add(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int id) {

            Product product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
