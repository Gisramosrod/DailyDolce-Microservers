using AutoMapper;
using DailyDolce.Web.Dtos;
using DailyDolce.Web.Dtos.Cart;
using DailyDolce.Web.Dtos.Product;
using DailyDolce.Web.Models;
using DailyDolce.Web.Services.Cart;
using DailyDolce.Web.Services.Product;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DailyDolce.Web.Controllers {
    public class HomeController : Controller {
        private readonly IMapper _mapper;
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(
            IMapper mapper,
            ILogger<HomeController> logger,
            IProductService productService,
            ICartService cartService
            ) {
            _mapper = mapper;
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index() {
            List<ProductDto> list = new();
            var response = await _productService.GetAllProducts<ResponseDto>("");
            if (response != null && response.Success)
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Data));
            return View(list);
        }

        public async Task<IActionResult> ProductDetails(int id) {
            ProductAndQuantityDto model = new();
            var response = await _productService.GetProductById<ResponseDto>(id, "");
            if (response != null && response.Success)
                model = JsonConvert.DeserializeObject<ProductAndQuantityDto>(Convert.ToString(response.Data));

            return View(model);
        }

        [HttpPost]
        [ActionName("ProductDetails")]
        [Authorize]
        public async Task<IActionResult> ProductDetailsPost(ProductAndQuantityDto productAndQuantityDto) {

            AddToCartDto addToCartDto = new() {
                UserId = User.Claims.Where(c => c.Type == "sub")?.FirstOrDefault()?.Value,
                ProductDto = _mapper.Map<ProductDto>(productAndQuantityDto),
                Quantity = productAndQuantityDto.Quantity
            };

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var addToCartResp = await _cartService.AddToCart<ResponseDto>(addToCartDto, accessToken);

            if (addToCartResp != null && addToCartResp.Success)
                return RedirectToAction(nameof(Index));

            return View(false);
        }

        [Authorize]
        public async Task<IActionResult> Login() {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout() {
            return SignOut("Cookies", "oidc");
        }


        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}