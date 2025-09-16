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

    public override string ToString()
    {
        return $"Код: {Code}\n" +
               $"Название: {Name}\n" +
               $"Цена: {Price:C}\n" +
               $"Количество: {Quantity}\n" +
               $"В наличии: {(IsInStock ? "Да" : "Нет")}\n" +
               $"Категория: {Category}\n" +
               new string('-', 40);
    }
}
class Program
{
    private static List<Store> products = new List<Store>();
    private static int NextCodeNumber = 1;
    static void Main(string[] args)
    {
        InitializeTestData();

        while (true)
        {
            ShowMenu();
            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        RemoveProduct();
                        break;
                    case "3":
                        OrderSupply();
                        break;
                    case "4":
                        SellProduct();
                        break;
                    case "5":
                        SearchProducts();
                        break;
                    case "6":
                        DisplayAllProducts();
                        break;
                    case "0":
                        Console.WriteLine("Выход из программы. До свидания!");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Пожалуйста, выберите пункт от 0 до 6.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}