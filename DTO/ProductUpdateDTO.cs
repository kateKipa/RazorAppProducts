namespace WebRazorAppProducts.DTO
{
    public class ProductUpdateDTO : BaseDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }
}
