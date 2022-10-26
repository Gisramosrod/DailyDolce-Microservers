using DailyDolce.Web.Dtos;
using DailyDolce.Web.Dtos.Product;
using DailyDolce.Web.Services.Product;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DailyDolce.Web.Controllers
{
    public class ProductController : Controller {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) {
            _productService = productService;
        }

        public async Task<IActionResult> ProductIndex() {

            List<ProductDto> products = new();

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetAllProducts<ResponseDto>(accessToken);

            if (response != null && response.Success)
                products = JsonConvert
                    .DeserializeObject<List<ProductDto>>(Convert.ToString(response.Data));

            return View(products);
        }
        public IActionResult CreateProduct() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductDto newProduct) {

            if (ModelState.IsValid) {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.CreateProduct<ResponseDto>(newProduct, accessToken);

                if (response != null && response.Success)
                    return RedirectToAction(nameof(ProductIndex));
            }

            return View(newProduct);
        }

        public async Task<IActionResult> EditProduct(int id) {

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductById<ResponseDto>(id, accessToken);

            if (response != null && response.Success) {
                ProductDto product = JsonConvert
               .DeserializeObject<ProductDto>(Convert.ToString(response.Data));

                return View(product);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(ProductDto updatedProduct) {

            if (ModelState.IsValid) {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.UpdateProduct<ResponseDto>(updatedProduct, accessToken);

                if (response != null && response.Success)
                    return RedirectToAction(nameof(ProductIndex));
            }

            return View(updatedProduct);
        }

        public async Task<IActionResult> DeleteProduct(int id) {

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductById<ResponseDto>(id, accessToken);

            if (response != null && response.Success) {
                ProductDto product = JsonConvert
               .DeserializeObject<ProductDto>(Convert.ToString(response.Data));

                return View(product);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduct(ProductDto deletedProduct) {

            if (ModelState.IsValid) {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.DeleteProduct<ResponseDto>(deletedProduct.Id, accessToken);

                if (response != null && response.Success)
                    return RedirectToAction(nameof(ProductIndex));
            }

            return View(deletedProduct);
        }
    }
}
