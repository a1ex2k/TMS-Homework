namespace ProductInventoryProject;

public class Product
{
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public string? Description { get; init; }


    public Product(decimal price) : this(price, 0, null)
    {
    }

    public Product(decimal price, string? description) : this(price, 0, description)
    {
    }

    public Product(decimal price, int quantity, string? description)
    {
        if (price < 0)
            throw new ArgumentException("Price was negative");

        Price = price;
        Description = description;
        Quantity = 0;
    }


    public void ChangeQuantity(int count)
    {
        if (count == 0) return;
        if (count < 0 && (-count) > Quantity)
            throw new ArgumentException("Count is greater than current quantity");
        Quantity += count;
    }


    public void DecreaseQuantity(int count)
    {
        if (count < 0)
            throw new ArgumentException("Count was negative");

        if (count > Quantity)
            throw new ArgumentException("Count is greater than current quantity");

        Quantity -= count;
    }


    public void SetNewPrice(decimal price)
    {
        if (price < 0)
            throw new ArgumentException("Price was negative");
        Price = price;
    }


    public override string ToString()
    {
        return $"{(string.IsNullOrWhiteSpace(Description) ? "<No Description>" : Description)} - Price: {Price:F2}, Quantity: {Quantity}";
    }
}