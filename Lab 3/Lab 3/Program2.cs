using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}

public class Program
{
    static void Main(string[] args)
    {
        
        Predicate<Product> priceFilter = product => product.Price < 10;
        Predicate<Product> quantityFilter = product => product.Quantity > 0;
        Predicate<Product> nameFilter = product => product.Name.Contains("apple");

        
        Action<Product> displayProduct = product =>
        {
            Console.WriteLine($"Name: {product.Name}\tPrice: {product.Price}\tQuantity: {product.Quantity}");
        };

        
        for (int i = 1; i <= 10; i++)
        {
            string filePath = $"products_{i}.json";

            
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(filePath));

            List<Product> filteredProducts = FilterProducts(products, priceFilter, quantityFilter, nameFilter);

            foreach (Product product in filteredProducts)
            {
                displayProduct(product);
            }
        }
    }

    static List<Product> FilterProducts(List<Product> products, params Predicate<Product>[] filters)
    {
        List<Product> filteredProducts = new List<Product>();

        foreach (Product product in products)
        {
            bool passesFilters = true;

            foreach (Predicate<Product> filter in filters)
            {
                if (!filter(product))
                {
                    passesFilters = false;
                    break;
                }
            }

            if (passesFilters)
            {
                filteredProducts.Add(product);
            }
        }

        return filteredProducts;
    }
}
