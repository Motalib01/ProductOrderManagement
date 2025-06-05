using ProductOrderManagement.Domain.Abstractions;
using ProductOrderManagement.Domain.Shared;

namespace ProductOrderManagement.Domain.Product;

public sealed class Product: Entity
{
    public Product(Guid id, Name name, Money price, Status status):base(id)
    {
        Name = name;
        Price = price;
        Status = status;
    }
    
    public Name Name{ get; private set; }
    public Money Price { get; private set; }
    public Status Status { get; private set; }
    
    public void UpdateStatus(Status status)
    {
        if (Status == status)
        {
            throw new InvalidOperationException("The product already has the specified status.");
        }

        Status = status;
    }
}