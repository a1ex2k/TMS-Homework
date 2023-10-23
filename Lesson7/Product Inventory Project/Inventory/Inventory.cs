using System.Collections;
using System.Text;

namespace ProductInventoryProject;

public class Inventory : IEnumerable<Product>
{
    private List<Product> _products;

    public Inventory()
    {
        _products = new List<Product>();    
    }

    public Inventory(IEnumerable<Product> products) : this()
    {
        Add(products);
    }

    public Product? GetById(int id)
    {
        if (id < 0 || id >= _products.Count)
        {
            return null;
        }

        return _products[id];
    }


    public int Add(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        
        _products.Add(product);
        return _products.Count;
    }


    public void Add(IEnumerable<Product> products)
    {
        foreach (var product in products)
        {
            Add(product);
        }
    }


    public bool TryRemove(Product product)
    {
        return _products.Remove(product);
    }


    public int Count => _products.Count;

    public decimal TotalPrice => _products.Sum(p => p.Quantity * p.Price);

    private List<Product> ToList() => _products.ToList();

    public IEnumerator<Product> GetEnumerator()
    {
        return _products.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}