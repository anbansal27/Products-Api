using FluentValidation.TestHelper;
using Products.Api.Application.Validators;
using Products.Api.Contracts;
using Products.Api.UnitTests.Builders;
using Xunit;

namespace Products.Api.UnitTests.Validators
{
    public class When_Validating_Product
    {
        private readonly ProductValidator _validator;

        public When_Validating_Product() => _validator = new ProductValidator();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void And_Code_Is_Not_Valid(string code)
        {
            // Arrange 
            var product = new ProductDto { Price = 10, Code = code, Description = "description", Name = "name" };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Code);
            Assert.True(!result.IsValid);
            Assert.True(result.Errors.Count == 1);
            Assert.Contains(result.Errors, x => x.PropertyName == "Code" && x.ErrorMessage == ValidationErrors.CodeRequiredError);
        }

        [Fact]
        public void And_Code_Overflows_Max_Length()
        {
            // Arrange             
            string invalidCode = RandomBuilder.NextString(ValidationLimits.CodeMaxLength + 1);
            var product = new ProductDto { Price = 10, Code = invalidCode, Description = "description", Name = "name" };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Code);
            Assert.True(!result.IsValid);
            Assert.True(result.Errors.Count == 1);
            Assert.Contains(result.Errors, x => x.PropertyName == "Code" && x.ErrorMessage == ValidationErrors.CodeMaxLengthError);
        }

        [Fact]
        public void And_Description_Overflows_Max_Length()
        {
            // Arrange             
            string invalidDescription = RandomBuilder.NextString(ValidationLimits.DescriptionMaxLength + 1);
            var product = new ProductDto { Price = 10, Code = "code", Description = invalidDescription, Name = "name" };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description);
            Assert.True(!result.IsValid);
            Assert.True(result.Errors.Count == 1);
            Assert.Contains(result.Errors, x => x.PropertyName == "Description" && x.ErrorMessage == ValidationErrors.DescriptionMaxLengthError);
        }

        [Fact]
        public void And_Delivery_Price_Is_Not_Valid()
        {
            // Arrange                         
            var product = new ProductDto { Price = 10, Code = "code", DeliveryPrice = -1, Description = "description", Name = "name" };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.DeliveryPrice);
            Assert.True(!result.IsValid);
            Assert.True(result.Errors.Count == 1);
            Assert.Contains(result.Errors, x => x.PropertyName == "DeliveryPrice" && x.ErrorMessage == ValidationErrors.DeliveryPriceMinValueError);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void And_Name_Is_Not_Valid(string name)
        {
            // Arrange 
            var product = new ProductDto { Price = 10, Code = "code", Description = "description", Name = name };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            Assert.True(!result.IsValid);
            Assert.True(result.Errors.Count == 1);
            Assert.Contains(result.Errors, x => x.PropertyName == "Name" && x.ErrorMessage == ValidationErrors.NameRequiredError);
        }

        [Fact]
        public void And_Name_Overflows_Max_Length()
        {
            // Arrange             
            string invalidName = RandomBuilder.NextString(ValidationLimits.NameMaxLength + 1);
            var product = new ProductDto { Price = 10, Code = "code", Description = "description", Name = invalidName };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            Assert.True(!result.IsValid);
            Assert.True(result.Errors.Count == 1);
            Assert.Contains(result.Errors, x => x.PropertyName == "Name" && x.ErrorMessage == ValidationErrors.NameMaxLengthError);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void And_Price_Is_Not_Valid(decimal price)
        {
            // Arrange 
            var product = new ProductDto { Price = price, Code = "code", Description = "description", Name = "name" };

            // Act
            var result = _validator.TestValidate(product);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Price);
            Assert.True(!result.IsValid);
            Assert.True(result.Errors.Count == 1);
            Assert.Contains(result.Errors, x => x.PropertyName == "Price" && x.ErrorMessage == ValidationErrors.PriceMinValueError);
        }
    }
}
