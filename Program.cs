using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
//Игрок
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
    public double HP { get; set; }
    public double Attack { get; set; }
    public double Defense { get; set; }

    public Enemy(double hp, double attack, double defense)
    {
        HP = hp;
        Attack = attack;
        Defense = defense;
    }
}
//Гоблины
public class Goblin : Enemy
{
    public double CritDamage { get; set; }
    public Goblin(double hp, double attack, double defense, double critdamage)
        : base(hp, attack, defense) 
    {
        HP = 80.0;
        Attack = 40;
        Defense = 20;
        CritDamage = 1.3;
    }
    public virtual void Critical()
    {
        Attack *= CritDamage; 
    }
}
public class ВВГ : Goblin
{
    public ВВГ(double hp, double attack, double defense, double critdamage)
        : base(hp, attack, defense, critdamage)
    {
        HP *= 2;
        Attack *= 1.5;
        Defense *= 1.2;
        CritDamage *= 1.1;
    }
    public override void Critical()
    {
        base.Critical();
    }
}
//Скелеты
public class Skeleton : Enemy
{
    public Skeleton(double hp, double attack, double defense)
        : base(hp, attack, defense)
    {
        HP = 60.0;
        Attack = 30;
        Defense = 15;
    }
    public virtual void IgnoreDef()
    {
        
    }
}
public class Ковальский : Skeleton
{
    public Ковальский(double hp, double attack, double defense)
        : base(hp, attack, defense)
    {
        HP *= 2.5;
        Attack *= 1.3;
        Defense *= 1.4;
    }
    public override void IgnoreDef()
    {
        base.IgnoreDef();
    }
}
//Маги
public class Wizard : Enemy
{
    public double FreezeChance { get; set; }
    public Wizard(double hp, double attack, double defense, double freezeChance)
        : base(hp, attack, defense)
    {
        HP = 70.0;
        Attack = 50;
        Defense = 0;
        FreezeChance = 50;
    }
    public virtual void Freeze() 
    { 
        
    }

}
public class АрхимагCPP : Wizard
{
    public АрхимагCPP(double hp, double attack, double defense, double freezeChance)
        : base(hp, attack, defense, freezeChance)
    {
        HP *= 1.8;
        Attack *= 1.6;
        Defense *= 1.1;
        FreezeChance *= 1.1;
    }
    public override void Freeze()
    {
        base.Freeze();
    }
}
public class ПестовCMM : Wizard
{
    public ПестовCMM(double hp, double attack, double defense, double freezeChance)
        : base(hp, attack, defense,  freezeChance)
    {
        HP *= 1.3;
        Attack *= 1.8;
        Defense *= 0.6;
    }
    public override void Freeze()
    {
        base.Freeze();
    }
}
//Предметы
public class Chest
{
    public string HealPotion { get; set; }
}
//Основной код игры
public class Game
{

}