using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities;

public sealed class Category : BaseEntity
{
    public string? Name { get; private set; }

    public ICollection<Product>? Products { get; set; }

    public Category(string name)
    {
        ValidateDomain(name);
    }

    public Category(int id, string name)
    {
        DomainExceptionValidation.When(id < 0, "Invalid Id");
        Id = id;

        ValidateDomain(name);
    }

    public void Update(string name)
    {
        ValidateDomain(name);
    }

    private void ValidateDomain(string name)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name, name is required");
        DomainExceptionValidation.When(name.Length < 3, "Invalid name, name must have minimum 3 charecters");
        Name = name;
    }
}
