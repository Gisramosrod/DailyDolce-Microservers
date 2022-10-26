using DailyDolce.Services.ProductApi.Dtos;
using DailyDolce.Services.ProductApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyDolce.Services.ProductApi.Controllers {
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase {
        private readonly IProductRepository _productRepository;
        protected ResponseDto _response;

        public ProductController(IProductRepository productRepository) {
            _productRepository = productRepository;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetProducts() {
            try {
                IEnumerable<ProductDto> products = await _productRepository.GetProducts();

                if (products == null) {
                    _response.Success = false;
                    _response.ErrorMessages.Add("No products found.");
                }

                _response.Data = products;

            } catch (Exception ex) {
                _response.Success = false;
                _response.ErrorMessages.Add(ex.Message);
            }

            return _response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetProductById(int id) {
            try {
                ProductDto product = await _productRepository.GetProduct(id);
                if (product == null) {
                    _response.Success = false;
                    _response.ErrorMessages.Add("No products found.");
                }

                _response.Data = product;

            } catch (Exception ex) {
                _response.Success = false;
                _response.ErrorMessages.Add(ex.Message);
            }

            return _response;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateProduct(ProductDto newProduct) {
            try {
                ProductDto product = await _productRepository.CreateUpdateProduct(newProduct);
                if (product == null) {
                    _response.Success = false;
                    _response.ErrorMessages.Add("No products found.");
                }

                _response.Data = product;

            } catch (Exception ex) {
                _response.Success = false;
                _response.ErrorMessages.Add(ex.Message);
            }

            return _response;
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ResponseDto>> UpdateProduct(ProductDto updatedProduct) {
            try {
                ProductDto product = await _productRepository.CreateUpdateProduct(updatedProduct);
                if (product == null) {
                    _response.Success = false;
                    _response.ErrorMessages.Add("No products found.");
                }

                _response.Data = product;

            } catch (Exception ex) {
                _response.Success = false;
                _response.ErrorMessages.Add(ex.Message);
            }

            return _response;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteProduct(int id) {
            try {
                bool result = await _productRepository.DeleteProduct(id);
                _response.Data = result;
                if (!result) {
                    _response.Success = false;
                    _response.ErrorMessages.Add("No products found.");
                }

            } catch (Exception ex) {
                _response.Success = false;
                _response.ErrorMessages.Add(ex.Message);
            }

            return _response;
        }
    }
}
