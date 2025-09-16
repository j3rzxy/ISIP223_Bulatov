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
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public bool IsInStock => Quantity > 0;
    public Category Category { get; set; }

    public Store(string code, string name, decimal price, int quantity, Category category)
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
    private static int quantity;

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
                        Add();
                        break;
                    case "2":
                        Delete();
                        break;
                    case "3":
                        Order();
                        break;
                    case "4":
                        Sell();
                        break;
                    case "5":
                        Research();
                        break;
                    case "6":
                        Display();
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
    static void InitializeTestData()
    {
        products.Add(new Store(GenerateCode(), "Смартфон", 29999.99m, 10, Category.Electronics));
        products.Add(new Store(GenerateCode(), "Хлеб", 45.50m, 50, Category.Groceries));
        products.Add(new Store(GenerateCode(), "Джинсы", 3500.00m, 15, Category.Clothing));
        products.Add(new Store(GenerateCode(), "Ноутбук", 89999.99m, 5, Category.Electronics));
        products.Add(new Store(GenerateCode(), "Молоко", 75.00m, 30, Category.Groceries));

        // Обновляем счётчик кодов
        NextCodeNumber = products.Count + 1;
    }

    static string GenerateCode()
    {
        return "1" + NextCodeNumber++.ToString("D4");
    }

    static void ShowMenu()
    {
        Console.WriteLine("=== УЧЁТ ТОВАРОВ В МАГАЗИНЕ ===");
        Console.WriteLine("1. Добавить товар");
        Console.WriteLine("2. Удалить товар");
        Console.WriteLine("3. Заказать поставку товара");
        Console.WriteLine("4. Продать товар");
        Console.WriteLine("5. Поиск товаров");
        Console.WriteLine("6. Показать все товары");
        Console.WriteLine("0. Выход");
        Console.Write("Выберите действие: ");
    }

    static void Add()
    {
        Console.WriteLine("\n--- ДОБАВЛЕНИЕ ТОВАРА ---");

        Console.WriteLine("Введите навзание товара: ");
        string name = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Название не может быть пустым.");
            return;
        }
        Console.WriteLine("Введите цену товара: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
        {
            Console.WriteLine("Количество должно быть неотрицательным целым числом.");
            return;
        }
        Console.WriteLine("Выберите категорию:");
        var categories = Enum.GetValues<Category>();
        for (int i = 0; i < categories.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {categories[i]}");
        }

        Console.Write("Введите номер категории: ");
        if (!int.TryParse(Console.ReadLine(), out int catChoice) || catChoice < 1 || catChoice > categories.Length)
        {
            Console.WriteLine("Неверный выбор категории.");
            return;
        }

        Category selectedCategory = categories[catChoice - 1];

        string code = GenerateCode();
        var product = new Store(code, name, price, quantity, selectedCategory);
        products.Add(product);

        Console.WriteLine("Товар успешно добавлен:");
        Console.WriteLine(product);
    }

    static void Delete()
    {
        Console.WriteLine("\n--- УДАЛЕНИЕ ТОВАРА ---");
        Console.WriteLine("Введите код товара для удаления: ");
        string code = Console.ReadLine()?.Trim();

        var product = products.FirstOrDefault(p => p.Code == code);
        if (product != null)
        {
            Console.WriteLine("Товар не найден.");
            return;
        }
        products.Remove(product);
        Console.WriteLine($"Товар {product.Name} удалён.");
    }
    static void Order()
    {
        Console.WriteLine("\n--- ЗАКАЗ ПОСТАВКИ ТОВАРА ---");
        Console.Write("Введите код поставки товара: ");
        string code = Console.ReadLine()?.Trim();

        var product = products.FirstOrDefault(p =>p.Code == code);
        if (product != null)
        {
            Console.WriteLine("Товар с таким кодом не найден.");
            return;
        }
        Console.Write("Введите количество для поставки: ");
            if (!int.TryParse(Console.ReadLine(), out int supplyQuantity) || supplyQuantity <= 0)
            {
                Console.WriteLine("Количество должно быть положительным числом.");
                return;
            }

            product.Quantity += supplyQuantity;
            Console.WriteLine($"Поставка успешно добавлена. Новое количество: {product.Quantity}");
    }
    static void Sell()
    {
        Console.WriteLine("\n--- ПРОДАЖА ТОВАРА ---");
        Console.Write("Введите код товара: ");
        string code = Console.ReadLine()?.Trim();

        var product = products.FirstOrDefault(p => p.Code == code);
        if (product == null)
        {
            Console.WriteLine("Товар с таким кодом не найден.");
            return;
        }

        if (!product.IsInStock)
        {
            Console.WriteLine("Товар отсутствует на складе.");
            return;
        }

        Console.Write("Введите количество для продажи: ");
        if (!int.TryParse(Console.ReadLine(), out int sellQuantity) || sellQuantity <= 0)
        {
            Console.WriteLine("Количество должно быть положительным числом.");
            return;
        }

        if (sellQuantity > product.Quantity)
        {
            Console.WriteLine($"Недостаточно товара на складе. Доступно: {product.Quantity}");
            return;
        }

        product.Quantity -= sellQuantity;
        Console.WriteLine($"Продажа успешно завершена. Остаток: {product.Quantity}");
    }

    static void Research()
    {
        Console.WriteLine("\n--- ПОИСК ТОВАРОВ ---");
        Console.WriteLine("1. По коду");
        Console.WriteLine("2. По названию");
        Console.WriteLine("3. По категории");
        Console.Write("Выберите способ поиска: ");

        string choice = Console.ReadLine();
        List<Store> results = new List<Store>();

        switch (choice)
        {
            case "1":
                Console.Write("Введите код товара: ");
                string code = Console.ReadLine()?.Trim();
                results = products.Where(p => p.Code == code).ToList();
                break;

            case "2":
                Console.Write("Введите название товара (или часть названия): ");
                string namePart = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(namePart))
                {
                    results = products.Where(p => p.Name.Contains(namePart, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                break;

            case "3":
                Console.WriteLine("Выберите категорию:");
                var categories = Enum.GetValues<Category>();
                for (int i = 0; i < categories.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {categories[i]}");
                }
                Console.Write("Введите номер категории: ");
                if (int.TryParse(Console.ReadLine(), out int catChoice) && catChoice >= 1 && catChoice <= categories.Length)
                {
                    Category selectedCategory = categories[catChoice - 1];
                    results = products.Where(p => p.Category == selectedCategory).ToList();
                }
                else
                {
                    Console.WriteLine("Неверный выбор категории.");
                    return;
                }
                break;

            default:
                Console.WriteLine("Неверный выбор способа поиска.");
                return;
        }

        if (results.Count == 0)
        {
            Console.WriteLine("Товары не найдены.");
        }
        else
        {
            Console.WriteLine($"\nНайдено товаров: {results.Count}");
            foreach (var product in results)
            {
                Console.WriteLine(product);
            }
        }
    }

    static void Display()
    {
        Console.WriteLine("\n--- ВСЕ ТОВАРЫ ---");
        if (products.Count == 0)
        {
            Console.WriteLine("Список товаров пуст.");
            return;
        }

        foreach (var product in products)
        {
            Console.WriteLine(product);
        }
    }
}
