namespace Products.Api.Validators
{
    public class ValidationErrors
    {
        public const string CodeRequiredError = "Code should not be empty.";

        public static string CodeMaxLengthError = $"Code cannot have more than {ValidationLimits.CodeMaxLength} characters.";

        public static string DeliveryPriceMinValueError = $"Delivery Price must be greater than or equal to {ValidationLimits.DeliveryPriceMinValue}.";

        public const string NameRequiredError = "Name should not be empty.";

        public static string NameMaxLengthError = $"Name cannot have more than {ValidationLimits.NameMaxLength} characters.";

        public static string DescriptionMaxLengthError = $"Description cannot have more than {ValidationLimits.DescriptionMaxLength} characters.";

        public static string PriceMinValueError = $"Price must be greater than {ValidationLimits.PriceMinValue}.";

        public const string ProductIdRequiredError = "ProductId should not be empty and a valid Guid.";
    }
}
