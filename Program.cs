using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;

public class Player
{
    public int HP { get; set; }
    public string Equipment { get; set; }
    public string Weapon { get; set; }

    public Player(int hp, string equipment, string weapon)
    {
        HP = 100;
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
}
public abstract class Enemy
{
    public int HP { get; set; }
    public double Attack { get; set; }
    public double Defense { get; set; }

    public Enemy(int hp, double attack, double defense)
    {
        HP = hp;
        Attack = attack;
        Defense = defense;
    }
}

public class Goblin : Enemy
{
    public double CritDamage { get; set; }
    public Goblin(int hp, double attack, double defense, double critdamage)
        : base(hp, attack, defense) 
    {
        HP = 80;
        Attack = 40;
        Defense = 20;
        CritDamage = 1.3;
    }
    public void Ability()
    {
        Attack *= CritDamage; 
    }
}

public class Skeleton : Enemy
{
    public Skeleton(int hp, double attack, double defense)
        : base(hp, attack, defense)
    {
        HP = 60;
        Attack = 30;
        Defense = 15;
    }
    public void Ability()
    {
        
    }
}

public class Wizard
{

}
public class Chest
{
    public string HealPotion { get; set; }
}

public class Game
{

}