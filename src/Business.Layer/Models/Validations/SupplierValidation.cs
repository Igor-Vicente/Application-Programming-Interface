using Business.Layer.Models.Validations.Documents;
using FluentValidation;

namespace Business.Layer.Models.Validations
{
    public class SupplierValidation : AbstractValidator<Supplier>
    {
        public SupplierValidation()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage("The {PropertyName} field must be provided.")
                .Length(2, 100).WithMessage("The {PropertyName} field must be between {MinLength} and {MaxLength} characters.");

            When(f => f.SupplierType == SupplierType.individual, () =>
            {
                RuleFor(f => f.Document.Length).Equal(CpfValidation.CpfSize)
                    .WithMessage("The Document field must be {ComparisonValue} characters and {PropertyValue} has been provided.");

                RuleFor(f => CpfValidation.Validate(f.Document)).Equal(true)
                    .WithMessage("The document provided is invalid.");
            });

            When(f => f.SupplierType == SupplierType.corporation, () =>
            {
                RuleFor(f => f.Document.Length).Equal(CnpjValidation.CnpjSize)
                    .WithMessage("The Document field must be {ComparisonValue} characters and {PropertyValue} has been provided.");

                RuleFor(f => CnpjValidation.Validate(f.Document)).Equal(true)
                    .WithMessage("The document provided is invalid.");
            });
        }
    }
}
