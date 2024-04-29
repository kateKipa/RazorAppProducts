﻿namespace WebRazorAppProducts.DTO
{
    public class ProductReadOnlyDTO : BaseDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }
}
