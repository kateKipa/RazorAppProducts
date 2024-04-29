using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazorAppProducts.DTO;
using WebRazorAppProducts.Models;
using WebRazorAppProducts.Services;

namespace WebRazorAppProducts.Pages.Products
{
    public class IndexModel : PageModel
    {
        public List<ProductReadOnlyDTO>? Products { get; set; } = new();
        public Error? ErrorObj { get; set; } = new();

        private readonly IMapper? _mapper;
        private readonly IProductService? _productService;

        public IndexModel(IMapper? mapper, IProductService? productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        public IActionResult OnGet()
        {
            try
            {
                ErrorObj = null;
                IList<Product>? products = _productService.GetAllProducts();
                foreach (var product in products)
                {
                    ProductReadOnlyDTO dto = _mapper!.Map<ProductReadOnlyDTO>(product);
                    Products!.Add(dto);
                }

            }catch (Exception ex)
            {
                ErrorObj = new Error("", ex.Message, "");
            }
            return Page();
        }
    }
}
