using FluentValidation;
using WebRazorAppProducts.DTO;

namespace WebRazorAppProducts.Validators
{
    public class ProductUpdateValidator : AbstractValidator<ProductUpdateDTO>
    {
        public ProductUpdateValidator()

        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Το όνομα προϊόντος είναι υποχρεωτικό")
                .Length(2, 50)
                .WithMessage("Το πεδίο 'Name' πρέπει να είναι μεταξύ 2 και 50 χαρακτήρες.");

            RuleFor(p => p.Price)
               .PrecisionScale(10, 2, false)
               .WithMessage("The decimal value must have a maximum of 2 digits after the decimal point.")
               .Must(x => decimal.TryParse(x.ToString(), out _))             
               .WithMessage("Price must be a valid number.");

        }
    }
}
