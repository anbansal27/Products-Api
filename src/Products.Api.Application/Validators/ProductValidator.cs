using FluentValidation;
using Products.Api.Application.Contracts;

namespace Products.Api.Application.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage(ValidationErrors.CodeRequiredError)
                .MaximumLength(ValidationLimits.CodeMaxLength)
                .WithMessage(ValidationErrors.CodeMaxLengthError);

            RuleFor(x => x.DeliveryPrice)
                .Custom((deliveryPrice, customContext) =>
                {
                    var product = customContext.InstanceToValidate;

                    if (product.DeliveryPrice != null)
                    {
                        if (product.DeliveryPrice < ValidationLimits.DeliveryPriceMinValue)
                        {
                            customContext.AddFailure(ValidationErrors.DeliveryPriceMinValueError);
                        }
                    }
                });

            RuleFor(x => x.Description)
                .MaximumLength(ValidationLimits.DescriptionMaxLength)
                .WithMessage(ValidationErrors.DescriptionMaxLengthError);

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidationErrors.NameRequiredError)
                .MaximumLength(ValidationLimits.NameMaxLength)
                .WithMessage(ValidationErrors.NameMaxLengthError);

            RuleFor(x => x.Price)
             .GreaterThan(ValidationLimits.PriceMinValue)
             .WithMessage(ValidationErrors.PriceMinValueError);
        }
    }
}
