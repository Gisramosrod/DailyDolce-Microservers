﻿using System.ComponentModel.DataAnnotations;

namespace DailyDolce.Services.ProductApi.Model {
    public class Product {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1,1000)]
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }

    }
}
