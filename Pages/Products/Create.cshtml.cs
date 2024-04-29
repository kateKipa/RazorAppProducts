using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebRazorAppProducts.DTO;
using WebRazorAppProducts.Models;
using WebRazorAppProducts.Services;

namespace WebRazorAppProducts.Pages.Products
{
    public class CreateModel : PageModel
    {
        public List<Error>? ErrorArray { get; set; } = new();
        public ProductInsertDTO ProductInsertDTO { get; set; } = new();

        //private readonly IMapper _mapper;
        private readonly IProductService? _productService;
        private readonly IValidator<ProductInsertDTO>? _validator;

        public CreateModel(IProductService? productService, IValidator<ProductInsertDTO>? validator)
        {
            _productService = productService;
            _validator = validator;
        }

        public void OnGet()
        {
        }

        public void OnPost(ProductInsertDTO dto)
        {
            ProductInsertDTO = dto;

            var validationResult = _validator!.Validate(dto);

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
                Product? product = _productService!.InsertProduct(dto);
                Response.Redirect("/Products/Index");
            } catch (Exception ex)
            {
                ErrorArray!.Add(new Error("", ex.Message, ""));
            }
        }
    }
}
