using FluentValidation.TestHelper;
using Products.Api.Application.Dto;
using Products.Api.UnitTests.Builders;
using Products.Api.Validators;
using System;
using Xunit;

namespace Products.Api.UnitTests.Validators
{
    public class When_Validating_Product_Option
    {
        private readonly ProductOptionValidator _validator;

        public When_Validating_Product_Option() => _validator = new ProductOptionValidator();

        [Fact]
        public void And_Description_Overflows_Max_Length()
        {
            // Arrange             
            string invalidDescription = RandomBuilder.NextString(ValidationLimits.DescriptionMaxLength + 1);
            var productOption = new ProductOptionDto { Description = invalidDescription, Name = "name", ProductId = RandomBuilder.NextGuid() };

            // Act
            var result = _validator.TestValidate(productOption);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Description);
            Assert.True(!result.IsValid);
            Assert.True(result.Errors.Count == 1);
            Assert.Contains(result.Errors, x => x.PropertyName == "Description" && x.ErrorMessage == ValidationErrors.DescriptionMaxLengthError);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void And_Name_Is_Not_Valid(string name)
        {
            // Arrange 
            var productOption = new ProductOptionDto { Description = "description", Name = name, ProductId = RandomBuilder.NextGuid() };

            // Act
            var result = _validator.TestValidate(productOption);

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
            var productOption = new ProductOptionDto { Description = "description", Name = invalidName, ProductId = RandomBuilder.NextGuid() };

            // Act
            var result = _validator.TestValidate(productOption);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            Assert.True(!result.IsValid);
            Assert.True(result.Errors.Count == 1);
            Assert.Contains(result.Errors, x => x.PropertyName == "Name" && x.ErrorMessage == ValidationErrors.NameMaxLengthError);
        }

        [Fact]
        public void And_ProductId_Is_Not_Valid()
        {
            // Arrange 
            var productOption = new ProductOptionDto { Description = "description", Name = "name", ProductId = Guid.Empty };

            // Act
            var result = _validator.TestValidate(productOption);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ProductId);
            Assert.True(!result.IsValid);
            Assert.True(result.Errors.Count == 1);
            Assert.Contains(result.Errors, x => x.PropertyName == "ProductId" && x.ErrorMessage == ValidationErrors.ProductIdRequiredError);
        }
    }
}
