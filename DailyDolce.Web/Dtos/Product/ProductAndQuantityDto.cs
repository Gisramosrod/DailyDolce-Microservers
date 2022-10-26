using System.ComponentModel.DataAnnotations;

namespace DailyDolce.Web.Dtos.Product
{
    public class ProductAndQuantityDto {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        [Range(1, 100)]
        public int Quantity { get; set; }

        public ProductAndQuantityDto() { Quantity = 1; }
    }
}
