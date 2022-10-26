using DailyDolce.Web.Dtos.Product;
using DailyDolce.Web.Models;
using DailyDolce.Web.Services.Base;

namespace DailyDolce.Web.Services.Product
{
    public class ProductService : BaseService, IProductService {

        public ProductService(IHttpClientFactory httpClient) : base(httpClient) {
        }

        public async Task<T> GetAllProducts<T>(string token) {
            return await SendRequest<T>(new ApiRequest() {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ProductApiBase}/api/products",
                AccessToken = token
            });
        }

        public async Task<T> GetProductById<T>(int id, string token) {
            return await SendRequest<T>(new ApiRequest() {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.ProductApiBase}/api/products/{id}",
                AccessToken = token
            });
        }

        public async Task<T> CreateProduct<T>(ProductDto newProduct, string token) {
            return await SendRequest<T>(new ApiRequest() {
                Data = newProduct,
                ApiType = SD.ApiType.POST,
                Url = $"{SD.ProductApiBase}/api/products",
                AccessToken = token
            });
        }

        public async Task<T> UpdateProduct<T>(ProductDto updatedProduct, string token) {
            return await SendRequest<T>(new ApiRequest() {
                Data = updatedProduct,
                ApiType = SD.ApiType.PUT,
                Url = $"{SD.ProductApiBase}/api/products",
                AccessToken = token
            });
        }

        public async Task<T> DeleteProduct<T>(int id, string token) {
            return await SendRequest<T>(new ApiRequest() {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.ProductApiBase}/api/products/{id}",
                AccessToken = token
            });
        }
    }
}
