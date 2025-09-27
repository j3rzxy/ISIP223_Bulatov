using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;
using System.Linq;

public enum Genre
{
    Detective,
    Novel,
    Story,
    Comedy,
    Tradegy,
    Fantasy,
    Prose,
    Classic,
    Antiutopia
}
class Book
{
    public string ID { get; set; }
    public string Title { get; set; }
    public string Author {  get; set; }
    public Genre Genre { get; set; }
    public int Date { get; set; }
    public int Price { get; set; }

    public Book(string id, string title, string author, Genre genre, int date, int price)
    {
        ID = id;
        Title = title ?? throw new ArgumentNullException(nameof(title));
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Название не может быть пустым.");
        if (price < 0) throw new ArgumentException("Цена не может быть отрицательной.");

        Title = title.Trim();
        Author = author;
        Genre = genre;
        Date = date;
        Price = price;
    }

    public Book(string id, string title, decimal price, Genre[] genres, Genre selectedGenre)
    {
        Title = title;
    }

    public override string ToString()
    {
        return $"Код: {ID}\n" +
               $"Название: {Title}\n" +
               $"Цена: {Price:C}\n" +
               $"Автор: {Author}\n" +
               $"Жанр: {Genre}\n" +
               $"Год издания: {Date}\n" +
               new string('-', 40);
    }
}
class Program
{
    private static List<Book> products = new List<Book>();
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
                        Add();
                        break;
                    case "2":
                        Delete();
                        break;
                    case "3":
                        Research();
                        break;/*
                    case "4":
                        Sort();
                        break;
                    case "5":
                        CheapEx();
                        break;
                    case "6":
                        Group();
                        break;*/
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

        static void InitializeTestData()
        {
            products.Add(new Book(GenerateCode(), "Властелин колец", "Джон Р. Р. Толкин", Genre.Fantasy, 2023, 250));
            products.Add(new Book(GenerateCode(), "Вторая жизнь Уве", "Фредрик Бакман", Genre.Prose, 2021, 300));
            products.Add(new Book(GenerateCode(), "Убить пересмешника", "Харпер Ли", Genre.Classic, 1960, 200));
            products.Add(new Book(GenerateCode(), "Гордость и предубеждение", "Джейн Остен", Genre.Novel, 1813, 190));
            products.Add(new Book(GenerateCode(), "1984", "Джордж Оруэлл", Genre.Antiutopia, 1949, 350));

            // Обновляем счётчик кодов
            NextCodeNumber = products.Count + 1;
        }
        static string GenerateCode()
        {
            return "1" + NextCodeNumber++.ToString("D4");
        }
        static void ShowMenu()
        {
            Console.WriteLine("=== УЧЁТ КНИГ В БИБЛИОТЕКЕ ===");
            Console.WriteLine("1. Добавить книгу");
            Console.WriteLine("2. Удалить книгу");
            Console.WriteLine("3. Поиск книги");
            Console.WriteLine("4. Сгруппировать книги по авторам и вывести количество книг каждого автора");
            Console.WriteLine("5. Отсортировать книги");
            Console.WriteLine("6. Показать самую дорогую и самую дешевую книги");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");
        }
        static void Add()
        {
            Console.WriteLine("\n--- ДОБАВЛЕНИЕ КНИГИ ---");

            Console.WriteLine("Введите название товара: ");
            string title = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Название не может быть пустым.");
                return;
            }
            Console.WriteLine("Введите автора книги: ");
            string author = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(author))
            {
                Console.WriteLine("Имя автора не может быть пустым.");
                return;
            }
            
            Console.WriteLine("Выберите жанр:");
            var genres = Enum.GetValues<Genre>();
            for (int i = 0; i < genres.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {genres[i]}");
            }

            Console.Write("Введите номер жанра: ");
            if (!int.TryParse(Console.ReadLine(), out int catChoice) || catChoice < 1 || catChoice > genres.Length)
            {
                Console.WriteLine("Неверный выбор категории.");
                return;
            }
            Console.WriteLine("Введите цену книги: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
            {
                Console.WriteLine("Количество должно быть неотрицательным целым числом.");
                return;
            }

            Genre selectedGenre = genres[catChoice - 1];

            string code = GenerateCode();
            var book = new Book(code, title, price, genres, selectedGenre);
            products.Add(book);

            Console.WriteLine("Товар успешно добавлен:");
            Console.WriteLine(book);
        }
        static void Delete()
        {
            Console.WriteLine("\n--- УДАЛЕНИЕ КНИГИ ---");
            Console.WriteLine("Введите код товара для удаления: ");
            string id = Console.ReadLine()?.Trim();

            var product = products.FirstOrDefault(p => p.ID == id);
            if (product != null)
            {
                Console.WriteLine("Товар не найден.");
                return;
            }
            products.Remove(product);
            Console.WriteLine($"Товар {product.Title} удалён.");
        }
        static void Research()
        {
            Console.WriteLine("\n--- ПОИСК КНИГИ ---");
            Console.WriteLine("1. По коду");
            Console.WriteLine("2. По названию");
            Console.WriteLine("3. По жанру");
            Console.Write("Выберите способ поиска: ");

            string choice = Console.ReadLine();
            List<Book> results = new List<Book>();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите код товара: ");
                    string id = Console.ReadLine()?.Trim();
                    results = products.Where(p => p.ID == id).ToList();
                    break;

                case "2":
                    Console.Write("Введите название товара (или часть названия): ");
                    string titlePart = Console.ReadLine()?.Trim();
                    if (!string.IsNullOrEmpty(titlePart))
                    {
                        results = products.Where(p => p.Title.Contains(titlePart, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    break;

                case "3":
                    Console.WriteLine("Выберите категорию:");
                    var genres = Enum.GetValues<Genre>();
                    for (int i = 0; i < genres.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {genres[i]}");
                    }
                    Console.Write("Введите номер жанра: ");
                    if (int.TryParse(Console.ReadLine(), out int catChoice) && catChoice >= 1 && catChoice <= genres.Length)
                    {
                        Genre selectedCategory = genres[catChoice - 1];
                        results = products.Where(p => p.Genre == selectedCategory).ToList();
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор жанра.");
                        return;
                    }
                    break;

                default:
                    Console.WriteLine("Неверный выбор способа поиска.");
                    return;
            }

            if (results.Count == 0)
            {
                Console.WriteLine("Книги не найдены.");
            }
            else
            {
                Console.WriteLine($"\nНайдено книг: {results.Count}");
                foreach (var product in results)
                {
                    Console.WriteLine(product);
                }
            }
        }
    }
}