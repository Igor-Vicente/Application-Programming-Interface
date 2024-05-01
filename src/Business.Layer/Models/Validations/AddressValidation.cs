using FluentValidation;

namespace Business.Layer.Models.Validations
{
    public class AddressValidation : AbstractValidator<Address>
    {
        public AddressValidation()
        {
            RuleFor(a => a.Street)
                  .NotEmpty().WithMessage("The {PropertyName} field must be provided. (Obrigatório!)")
                  .Length(2, 100).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters. (Inválido!)");

            RuleFor(a => a.Neighborhood)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided. (Obrigatório!)")
                .Length(2, 100).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters. (Inválido!)");

            RuleFor(a => a.PostalCode)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided. (Obrigatório!)")
                .Length(8).WithMessage("the {PropertyName} field must be {MaxLength} characters. (Inválido!)");

            RuleFor(a => a.City)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided. (Obrigatório!)")
                .Length(2, 100).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters. (Inválido!)");

            RuleFor(a => a.State)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided. (Obrigatório!)")
                .Length(2, 50).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters. (Inválido!)");

            RuleFor(a => a.StreetNumber)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided. (Obrigatório!)")
                .Length(1, 50).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters. (Inválido!)");
        }
    }
}
