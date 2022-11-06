using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class ProductUnityTest1
{
    [Fact(DisplayName = "Create Product With Valid State")]
    public void CreateProduct_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Product(1, "Product Name", "Product Descroption", 9.50m, 99, "Product Image");
        action.Should()
            .NotThrow<DomainExceptionValidation>();
    }

    [Fact(DisplayName = "Create Product With Invalid Id")]
    public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Product(-1, "Product Name", "Product Descroption", 9.50m, 99, "Product Image");
        action.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid Id");
    }

    [Fact(DisplayName = "Create Product With Short Name Value")]
    public void CreateProduct_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () => new Product(1, "Pr", "Product Descroption", 9.50m, 99, "Product Image");
        action.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid name, name must have minimum 3 charecters");
    }

    [Fact(DisplayName = "Create Product With Null Image")]
    public void CreateProduct_WithNullImageName_NoDomainException()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
        action.Should().NotThrow<DomainExceptionValidation>();
    }

    [Fact(DisplayName = "Create Product With Null Image")]
    public void CreateProduct_WithNullImageName_NoNullReferenceException()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
        action.Should().NotThrow<NullReferenceException>();
    }

    [Fact(DisplayName = "Create Product Without Image")]
    public void CreateProduct_WithEmptyImageName_NoDomainException()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "");
        action.Should().NotThrow<DomainExceptionValidation>();
    }

    [Fact(DisplayName = "Create Product With Invalid Price")]
    public void CreateProduct_InvalidPriceValue_DomainException()
    {
        Action action = () => new Product(1, "Product Name", "Product Description", -9.99m,
            99, "");
        action.Should().Throw<DomainExceptionValidation>()
             .WithMessage("Invalid price");
    }

    [Theory(DisplayName = "Create Product With Invalid Stock Value")]
    [InlineData(-5)]
    public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
    {
        Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, value,
            "product image");
        action.Should().Throw<DomainExceptionValidation>()
               .WithMessage("Invalid stock");
    }
}
