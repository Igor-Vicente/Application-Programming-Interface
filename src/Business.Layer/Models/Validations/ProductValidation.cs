using FluentValidation;

namespace Business.Layer.Models.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided.")
                .Length(2, 100).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters.");

            RuleFor(p => p.Description)
               .NotEmpty().WithMessage("The {PropertyName} field must be provided.")
               .Length(2, 1000).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters.");

            RuleFor(p => p.Value)
                .GreaterThan(0).WithMessage("The {PropertyName} field must be greater than 0.");
        }

    }
}
