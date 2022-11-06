using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class CategoryUnitTest1
{
    [Fact(DisplayName = "Create Category With Valid State")]
    public void CreateCategory_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Category(1, "Category Name");
        action.Should()
            .NotThrow<DomainExceptionValidation>();
    }

    [Fact(DisplayName = "Create Category With Id Invalid")]
    public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Category(-1, "Category Name");
        action.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid Id");
    }

    [Fact(DisplayName = "Create Category With Short Name Value")]
    public void CreateCategory_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () => new Category(1, "Ca");
        action.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid name, name must have minimum 3 charecters");
    }

    [Fact(DisplayName = "Create Category Without Name")]
    public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
    {
        Action action = () => new Category(1, "");
        action.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid name, name is required");
    }

    [Fact(DisplayName = "Create Category With Null Name")]
    public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
    {
        Action action = () => new Category(1, null);
        action.Should()
            .Throw<DomainExceptionValidation>();
    }
}
