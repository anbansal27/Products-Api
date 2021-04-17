namespace Products.Api.Application.Validators
{
    public class ValidationErrors
    {
        public static string CodeRequiredError = $"Code should not be empty.";

        public static string CodeMaxLengthError = $"Code cannot have more than {ValidationLimits.CodeMaxLength} characters.";

        public static string DeliveryPriceMinValueError = $"Delivery Price must be greater than or equal to {ValidationLimits.DeliveryPriceMinValue}.";

        public static string NameRequiredError = $"Name should not be empty.";

        public static string NameMaxLengthError = $"Name cannot have more than {ValidationLimits.NameMaxLength} characters.";

        public static string DescriptionMaxLengthError = $"Description cannot have more than {ValidationLimits.DescriptionMaxLength} characters.";

        public static string PriceMinValueError = $"Price must be greater than {ValidationLimits.PriceMinValue}.";
    }
}
