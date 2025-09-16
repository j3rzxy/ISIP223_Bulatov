using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreInventoryApp;
public enum Category
{
    Electronics,
    Groceries,
    Clothing
}
class Store
{
    public int Code { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public bool IsInStock => Quantity > 0;
    public Category Category { get; set; }

    public Store(int code, string name, decimal price, int quantity, Category category)
    {
        Code = code;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Название не может быть пустым.");
        if (price < 0) throw new ArgumentException("Цена не может быть отрицательной.");
        if (quantity < 0) throw new ArgumentException("Количество не может быть отрицательным.");

        Name = name.Trim();
        Price = price;
        Quantity = quantity;
        Category = category;
    }

    public void Add()
    {

    }

    public void Delete()
    {

    }

    public void Order()
    {

    }

    public void Sell()
    {

    }

    public void Research()
    {

    }
}