using DailyDolce.Services.ProductApi.Dtos;

namespace DailyDolce.Services.ProductApi.Repository {
    public interface IProductRepository {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProduct(int id);
        Task<ProductDto> CreateUpdateProduct (ProductDto productDto);
        Task<bool> DeleteProduct(int id);
    }
}
