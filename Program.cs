using System;
using System.Collections.Generic;
using System.Linq;

public class Player
{
    public string HP { get; set; }
    public string Equipment {  get; set; }
    public string Weapon {  get; set; }

    public Player(string hp, string equipment, string weapon)
    {
        HP = hp;
        Equipment = equipment;
        Weapon = weapon;
    }

    public void PlayerStep()
    {
        Console.WriteLine($"Выберите стратегию хода:");
        Console.WriteLine("1. Атака");
        Console.WriteLine("2. Защита");
        if (!int.TryParse(Console.ReadLine(), out int StepChoice) || StepChoice != 1 || StepChoice != 2)
        {
            Console.WriteLine("Ошибка: введите либо 1, либо 2 для выбора стратегии.");
            Console.ReadKey();
            return;
        }
        if (StepChoice == 1) 
        {
            Console.WriteLine("-- АТАКА! --");

        }
        if (StepChoice == 2)
        {
            Console.WriteLine("-- ЗАЩИТА! --");

        }
}
public class Enemy
{

}