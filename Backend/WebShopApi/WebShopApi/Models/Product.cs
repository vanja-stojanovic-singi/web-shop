﻿namespace WebShopApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public Category? Category { get; set; }
        public Brand? Brand { get; set; }
        public virtual ICollection<ProductItem>? Items { get; set; }
        public virtual double? AvgRating { get; set; }
    }
}
