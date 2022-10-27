using DailyDolce.Web.Services.Product;
using DailyDolce.Web;
using DailyDolce.Web.Services.Cart;
using Microsoft.AspNetCore.Authentication;
using DailyDolce.Web.Services.Coupon;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Product Api
SD.ProductApiBase = builder.Configuration
    .GetSection("ServiceUrls").GetSection("ProductApi").Value;
builder.Services.AddHttpClient<IProductService, ProductService>();
builder.Services.AddScoped<IProductService, ProductService>();

//Shopping Cart Api
SD.ShoppingCartApiBase = builder.Configuration
    .GetSection("ServiceUrls").GetSection("ShoppingCartApi").Value;
builder.Services.AddHttpClient<ICartService, CartService>();
builder.Services.AddScoped<ICartService, CartService>();

//Coupon Api
SD.CouponApiBase = builder.Configuration
    .GetSection("ServiceUrls").GetSection("CouponApi").Value;
builder.Services.AddHttpClient<ICouponService, CouponService>();
builder.Services.AddScoped<ICouponService, CouponService>();

//Authentication
builder.Services.AddAuthentication(options => {
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
.AddOpenIdConnect("oidc", options => {
    options.Authority = builder.Configuration.GetSection("ServiceUrls").GetSection("IdentityApi").Value;
    options.GetClaimsFromUserInfoEndpoint = true;
    options.ClientId = "dailyDolce";
    options.ClientSecret = "secret";
    options.ResponseType = "code";
    options.ClaimActions.MapJsonKey("role", "role", "role");
    options.ClaimActions.MapJsonKey("sub", "sub", "sub");
    options.TokenValidationParameters.NameClaimType = "name";
    options.TokenValidationParameters.RoleClaimType = "role";
    options.Scope.Add("dailyDolce");
    options.SaveTokens = true;
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
