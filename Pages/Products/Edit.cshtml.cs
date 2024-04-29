using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazorAppProducts.DTO;
using WebRazorAppProducts.Models;
using WebRazorAppProducts.Services;

namespace WebRazorAppProducts.Pages.Products
{
    public class EditModel : PageModel
    {
        public ProductUpdateDTO? ProductUpdateDTO { get; set; } = new();
        public List<Error> ErrorArray { get; set; } = new();

        private readonly IProductService? _productService;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductUpdateDTO> _validator;

        public EditModel(IProductService? productService, IMapper mapper, IValidator<ProductUpdateDTO> validator)
        {
            _productService = productService;
            _mapper = mapper;
            _validator = validator;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                Product? product = _productService!.GetProduct(id);
                ProductUpdateDTO = _mapper.Map<ProductUpdateDTO>(product);
            } catch (Exception ex)
            {
                ErrorArray.Add(new Error("", ex.Message, ""));
            }
            return Page();
            
        }

        public void OnPost(ProductUpdateDTO dto) 
        {
            //ProductUpdateDTO = dto;

            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ErrorArray.Add(new Error(error.ErrorCode, error.ErrorMessage, error.PropertyName));
                }
                return;
            }

            try
            {
                Product? product = _productService!.UpdateProduct(dto);
                Response.Redirect("/Products/Index/");
            } catch (Exception e)
            {
                ErrorArray.Add(new Error("", e.Message, ""));
            }
        }
    }
}
