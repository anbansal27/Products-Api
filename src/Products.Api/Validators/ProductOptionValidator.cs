using FluentValidation;
using Products.Api.Application.Dto;

namespace Products.Api.Validators
{
    public class ProductOptionValidator : AbstractValidator<ProductOptionDto>
    {
        public ProductOptionValidator()
        {
            RuleFor(x => x.Description)
              .MaximumLength(ValidationLimits.DescriptionMaxLength)
              .WithMessage(ValidationErrors.DescriptionMaxLengthError);

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidationErrors.NameRequiredError)
                .MaximumLength(ValidationLimits.NameMaxLength)
                .WithMessage(ValidationErrors.NameMaxLengthError);

            RuleFor(x => x.ProductId)
              .NotEmpty()
              .WithMessage(ValidationErrors.ProductIdRequiredError);
        }
    }
}
