using FluentValidation;
using System.Data;
using WebRazorAppProducts.DTO;

namespace WebRazorAppProducts.Validators
{
    public class ProductInsertValidator : AbstractValidator<ProductInsertDTO>
    {
        public ProductInsertValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Το όνομα προϊόντος είναι υποχρεωτικό")
                .Length(2,50)
                .WithMessage("Το πεδίο 'Name' πρέπει να είναι μεταξύ 2 και 50 χαρακτήρες.");

            RuleFor(p => p.Price)
                .PrecisionScale(10, 2, false)
                .WithMessage("The decimal value must have a maximum of 2 digits after the decimal point.")
                .Must(x => decimal.TryParse(x.ToString(), out _))               // Ensure Price can be parsed to a decimal
                .WithMessage("Price must be a valid number.");

        }
    }
}
