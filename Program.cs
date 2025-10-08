using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;

public class Item
{
    public string Name { get; set; }
    public int Attack {  get; set; }
    public int Defense {  get; set; }
}
//Игрок
public class Player
{
    public double HP { get; set; } = 100;
    public double MaxHP { get; set; } = 100;
    public Item Weapon { get; set; } = new Item { Name = "Железный меч", Attack = 25 };
    public Item Armor { get; set; } = new Item { Name = "Железные доспехи", Defense = 25 };
    public bool IsFrozen { get; set; } = false;

    public void Heal() => HP = MaxHP;
    public int GetTotalAttack() => Weapon?.Attack ?? 0;
    public int GetTotalDefense() => Armor?.Defense ?? 0;
}
//Враг
public class Enemy
{
    public string Name { get; set; }
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public string Type { get; set; }
}

public class Boss : Enemy
{
    public int CritChanceBonus { get; set; }
    public int FreezeChanceBonus { get; set; }
}

public class Game
{
    private Player player = new Player();
    private Random rand = new Random();
    private int turn = 1;

    public void Start()
    {
        while (player.HP > 0)
        {
            Console.WriteLine();
            if (turn % 10 == 0)
            {

            }
            else
            {
                if (rand.Next(2) == 0)
                {

                }
                else
                {

                }
            }
            turn++;
            Console.WriteLine($"Ваше здоровье: {player.HP}/{player.MaxHP}");
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
            Console.Clear();
        }

        Console.WriteLine("Вы проиграли! Игра окончена.");
    }

    private void FindChest()
    {

    }

    private Enemy GetRandomEnemy()
    {
        var enemies = new Enemy[]
        {
                new Enemy { Name = "Гоблин", Type = "Goblin", HP = 30, MaxHP = 30, Attack = 12, Defense = 3 },
                new Enemy { Name = "Скелет", Type = "Skeleton", HP = 25, MaxHP = 25, Attack = 10, Defense = 5 },
                new Enemy { Name = "Маг", Type = "Mage", HP = 20, MaxHP = 20, Attack = 9, Defense = 2 }
        };
        return enemies[rand.Next(enemies.Length)];
    }

    private Boss GetRandomBoss()
    {
        var bosses = new Boss[]
        {
                new Boss { Name = "ВВГ", Type = "Goblin", HP = 60, MaxHP = 60, Attack = 18, Defense = 3, CritChanceBonus = 10 },
                new Boss { Name = "Ковальский", Type = "Skeleton", HP = 62, MaxHP = 62, Attack = 13, Defense = 7 },
                new Boss { Name = "Архимаг C++", Type = "Mage", HP = 36, MaxHP = 36, Attack = 14, Defense = 2, FreezeChanceBonus = 10 },
                new Boss { Name = "Пестов С--", Type = "Skeleton", HP = 32, MaxHP = 32, Attack = 18, Defense = 3, FreezeChanceBonus = 15 }
        };
        return bosses[rand.Next(bosses.Length)];
    }
}
class Program
{
    static void Main(string[] args)
    {
        var game = new Game();
        game.Start();
    }
}