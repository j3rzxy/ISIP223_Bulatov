using System.ComponentModel.DataAnnotations;

string o = Console.ReadLine();
int op = Convert.ToInt32(o); //Количество операций

string[] strs = new string[op];
int[] numbers = new int[op];

for (int i = 0; i < op; i++)//Заполнение массива и прием вводных данных
{
    string text = Console.ReadLine();
    string[] wan = text.Split(new char[] { ';' });
    strs[i] = wan[0];
    numbers[i] = Convert.ToInt32(wan[1]);
}

//1. Вывод данных
void Print()
{
    foreach (string str in strs)
    {
        Console.WriteLine(str);
    }
    foreach (int number in numbers)
    {
        Console.WriteLine(number);
    }
}

//2. Статистика (среднее, максимальное, минимальное, сумма)
void Statist()
{

    int avg = 0;
    for (int i = 0; i < numbers.Length; i++)
    {
        avg += numbers[i];
    }
    int max = numbers.Max();
    int min = numbers.Min();
    Console.WriteLine("Среднее:", avg / numbers.Length);
    Console.WriteLine("Максимальное:", max);
    Console.WriteLine("Минимальное:", min);
    Console.WriteLine("Сумма:", avg);
}

//3. Сортировка по цене (пузырьковая сортировка)


//4. Конвертация валюты (пользователь вводит курс или выбирает из списка)

//5. Поиск по названию

//0. Выход