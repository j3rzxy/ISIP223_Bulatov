using System;
using System.ComponentModel.DataAnnotations;

string o = Console.ReadLine();
int op = Convert.ToInt32(o); //Количество операций

string[] strings = new string[op];
int[] numbers = new int[op];

for (int i = 0; i < op; i++)//Заполнение массива и прием вводных данных
{
    string text = Console.ReadLine();
    string[] wan = text.Split(new char[] { ';' });
    strings[i] = wan[0];
    numbers[i] = Convert.ToInt32(wan[1]);
}

// Главное меню
int choice;
do
{
    Console.WriteLine("\n=== МЕНЮ ===");
    Console.WriteLine("1. Вывод данных");
    Console.WriteLine("2. Статистика (среднее, максимальное, минимальное, сумма)");
    Console.WriteLine("3. Сортировка по цене (пузырьковая сортировка)");
    Console.WriteLine("4. Конвертация валюты (пользователь вводит курс или выбирает из списка)");
    Console.WriteLine("5. Поиск по названию");
    Console.WriteLine("0. Выход");
    Console.Write("Выберите пункт меню: ");

    if (!int.TryParse(Console.ReadLine(), out choice))
    {
        Console.WriteLine("Ошибка! Введите число от 0 до 5.");
        continue;
    }

    switch (choice)
    {
        case 1:
            Print(numbers, strings);
            break;
        case 2:
            Statist(numbers);
            break;
        case 3:
            SortArrays(numbers, strings);
            break;
        case 4:
            Converting(numbers);
            break;
        case 5:
            Research(strings, numbers);
            break;
        case 0:
            Console.WriteLine("До свидания!");
            break;
        default:
            Console.WriteLine("Неверный выбор! Введите число от 0 до 5.");
            break;
    }
} while (choice != 0);

//1. Вывод данных
static void Print(int[] numbers, string[] strings)
{
    foreach (string str in strings)
    {
        Console.WriteLine(str);
    }
    foreach (int number in numbers)
    {
        Console.WriteLine(number);
    }
}

//2. Статистика (среднее, максимальное, минимальное, сумма)
static void Statist(int[] numbers)
{

    int avg = 0;
    for (int i = 0; i < numbers.Length; i++)
    {
        avg += numbers[i];
    }
    int max = numbers.Max();
    int min = numbers.Min();
    Console.WriteLine($"Среднее: {avg / numbers.Length}");
    Console.WriteLine($"Максимальное: {max}");
    Console.WriteLine($"Минимальное: {min}");
    Console.WriteLine($"Сумма: {avg}");
}

//3. Сортировка по цене (пузырьковая сортировка)
static void SortArrays(int[] numbers, string[] strings)
{
    var pairs = new List<Tuple<int, string>>();
    for (int i = 0; i < numbers.Length; i++)
    {
        pairs.Add(Tuple.Create(numbers[i], strings[i]));
    }

    pairs.Sort((a, b) => a.Item1.CompareTo(b.Item1));

    Console.WriteLine("Данные отсортированы по возрастанию цены.");

    for (int i = 0; i < pairs.Count; i++)
    {
        numbers[i] = pairs[i].Item1;
        strings[i] = pairs[i].Item2;
    }
    foreach (string str in strings)
    {
        Console.WriteLine(str);
    }
    foreach (int number in numbers)
    {
        Console.WriteLine(number);
    }
}

//4. Конвертация валюты (пользователь вводит курс или выбирает из списка)
static void Converting(int[] numbers)
{
    Console.WriteLine("Выберите сумму из списка:");
    for (int i = 0; i < numbers.Length; i++)
    {
        Console.WriteLine(numbers[i]);
    }
    string b1 = Console.ReadLine();
    float b = Convert.ToInt32(b1);
    Console.WriteLine("Введите курс валюты:");
    string c1 = Console.ReadLine();
    float c = Convert.ToInt32(c1);
    Console.WriteLine($"Конвертированная стоимость: {b * c} ");
}
//5. Поиск по названию
static void Research(string[] strings, int[] numbers)
{
    Console.WriteLine("Введите название позиции:");
    string f = Console.ReadLine();
    int ind = Array.IndexOf(strings, f);
    Console.WriteLine($"Найдено: { f } , { numbers[ind] }");
}
//0. Выход
static void Exit()
{
    Environment.Exit(0);
}
