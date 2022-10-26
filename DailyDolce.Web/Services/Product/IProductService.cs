using DailyDolce.Web.Dtos.Product;
using DailyDolce.Web.Services.Base;

namespace DailyDolce.Web.Services.Product
{
    public interface IProductService : IBaseService {
        Task<T> GetAllProducts<T>(string token);
        Task<T> GetProductById<T>(int id, string token);
        Task<T> CreateProduct<T>(ProductDto newProduct, string token);
        Task<T> UpdateProduct<T>(ProductDto updatedProduct, string token);
        Task<T> DeleteProduct<T>(int id, string token);
    }
}
