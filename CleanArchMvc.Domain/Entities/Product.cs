using CleanArchMvc.Domain.Validation;
using System.Runtime.CompilerServices;

namespace CleanArchMvc.Domain.Entities;

public sealed class Product : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string Image { get; private set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public Product(string name, string description, decimal price, int stock, string image)
    {
        ValidateDomain(name, description, price, stock, image);
    }

    public Product(int id, string name, string description, decimal price, int stock, string image)
    {
        DomainExceptionValidation.When(id < 0, "Invalid Id");
        Id = id;

        ValidateDomain(name, description, price, stock, image);
    }

    public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
    {
        ValidateDomain(name, description, price, stock, image);
        CategoryId = categoryId;
    }

    private void ValidateDomain(string name, string description, decimal price, int stock, string image)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid name, name is required");
        DomainExceptionValidation.When(name.Length < 3, "Invalid name, name must have minimum 3 charecters");
        DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Invalid description, description is required");
        DomainExceptionValidation.When(name.Length < 5, "Invalid description, description must have minimum 5 charecters");
        DomainExceptionValidation.When(price < 0, "Invalid price");
        DomainExceptionValidation.When(stock < 0, "Invalid stock");
        DomainExceptionValidation.When(image?.Length > 250, "Invalid image, image must have maximum 250 charecters");

        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        Image = image;
    }
}
