using System;
using System.Collections.Generic;
using System.Text;

class TextStatistics
{
    public int WordCount { get; set; }
    public string ShortestWord { get; set; }
    public int SentenceCount { get; set; }
    public int VowelCount { get; set; }
    public int ConsonantCount { get; set; }
    public string LongestWord { get; set; }
    public Dictionary<char, int> LetterFrequency { get; set; }

    static TextStatistics ProcessText(string text)
    {// === Сборка слов из текста ===
        // Алгоритм: проходим по каждому символу, собираем последовательности букв
        // Знаки препинания и пробелы используются как разделители
        List<string> words = new List<string>();
        StringBuilder currentWord = new StringBuilder();

        foreach (char c in text)
        {
            if (char.IsLetter(c))
            {
                // Если символ - буква, добавляем к текущему слову
                currentWord.Append(c);
            }
            else
            {
                // Если встретился не-буквенный символ и текущее слово не пустое
                if (currentWord.Length > 0)
                {
                    words.Add(currentWord.ToString());
                    currentWord.Clear();
                }
            }
        }

        // Добавляем последнее слово, если текст закончился буквой
        if (currentWord.Length > 0)
        {
            words.Add(currentWord.ToString());
        }

        // === Поиск самого короткого и самого длинного слов ===
        string shortestWord = null;
        string longestWord = null;
        int wordCount = words.Count;

        if (wordCount > 0)
        {
            shortestWord = words[0];
            longestWord = words[0];

            foreach (string word in words)
            {
                // Поиск самого короткого слова
                if (word.Length < shortestWord.Length)
                {
                    shortestWord = word;
                }
                // Поиск самого длинного слова
                if (word.Length > longestWord.Length)
                {
                    longestWord = word;
                }
            }
        }

        // === Подсчёт предложений ===
        // Предложения определяются по знакам окончания: . ! ?
        int sentenceCount = 0;
        foreach (char c in text)
        {
            if (c == '.' || c == '!' || c == '?')
            {
                sentenceCount++;
            }
        }

        // === Подсчёт гласных и согласных ===
        // Русские гласные буквы (регистронезависимо)
        string vowels = "аеёиоуыэюя";
        int vowelCount = 0;
        int consonantCount = 0;

        foreach (char c in text)
        {
            if (char.IsLetter(c))
            {
                char lowerC = char.ToLower(c);
                if (vowels.Contains(lowerC))
                {
                    vowelCount++;
                }
                else
                {
                    consonantCount++;
                }
            }
        }
    }
    
class Program
{
    static void Main()
    {
        List<TextStatistics> history = new List<TextStatistics>();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nВведите текст (минимум 100 символов):");
            string input = Console.ReadLine();

            // Проверка длины текста (минимум 100 символов)
            if (input.Length < 100)
            {
                Console.WriteLine("Ошибка: текст должен содержать не менее 100 символов.");
                continue;
            }

            // Обработка текста и сохранение статистики
            TextStatistics stats = ProcessText(input);
            history.Add(stats);

            // Вывод статистики текущего текста
            Console.WriteLine("\n=== Статистика по текущему тексту ===");
            PrintStatistics(stats);

            // Меню действий после обработки
            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1 - Обработать новый текст");
                Console.WriteLine("2 - Показать историю обработки");
                Console.WriteLine("3 - Выйти из программы");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine().Trim().ToLower();

                if (choice == "1")
                {
                    ;
                }
                else if (choice == "2")
                {
                    ;
                }
                else if (choice == "3")
                {
                    exit = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                }
            }
        }

        Console.WriteLine("\nДо свидания!");
    }
}