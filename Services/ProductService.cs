using AiaAuth.Models;

namespace AiaAuth.Services;

public class ProductService
{
    private readonly List<Product> _products = new List<Product>
    {
        new Product { Id = 1, Title = "Product1" },
        new Product { Id = 2, Title = "Product2" },
        new Product { Id = 3, Title = "Product3" },
    };

    public List<Product> GetProducts() => _products;
}
