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
    public Item Weapon { get; set; } = new Item { Name = "Железный меч", Attack = 15 };
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
        Console.WriteLine("Добро пожаловать в текстовую RPG!");
        while (player.HP > 0)
        {
            Console.WriteLine();
            if (turn % 10 == 0)
            {
                Fight(GetRandomBoss);
            }
            else
            {
                if (rand.Next(2) == 0)
                {
                    Chest();
                }
                else
                {
                    Fight(GetRandomEnemy);
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

    private void Chest()
    {
        Console.WriteLine("Вы получили сундук!");
        var items = new Item[]
        {
            new Item { Name = "Зелье здоровья", Attack = 0, Defense = 0 },
                new Item { Name = "Острый меч", Attack = 20, Defense = 0 },
                new Item { Name = "Кольчуга", Attack = 0, Defense = 35 },
                new Item { Name = "Клинок Дракона", Attack = 35, Defense = 0 },
                new Item { Name = "Доспехи Легиона", Attack = 0, Defense = 50 }
        };
        var item = items[rand.Next(items.Length)];
    }

    private Enemy GetRandomEnemy()
    {
        var enemies = new Enemy[]
        {
                new Enemy { Name = "Гоблин", Type = "Goblin", HP = 30, MaxHP = 30, Attack = 12, Defense = 3 },
                new Enemy { Name = "Скелет", Type = "Skeleton", HP = 25, MaxHP = 25, Attack = 10, Defense = 5 },
                new Enemy { Name = "Маг", Type = "Wizard", HP = 20, MaxHP = 20, Attack = 9, Defense = 2 }
        };
        return enemies[rand.Next(enemies.Length)];
    }

    private Boss GetRandomBoss()
    {
        var bosses = new Boss[]
        {
                new Boss { Name = "ВВГ", Type = "Goblin", HP = 60, MaxHP = 60, Attack = 18, Defense = 3, CritChanceBonus = 10 },
                new Boss { Name = "Ковальский", Type = "Skeleton", HP = 62, MaxHP = 62, Attack = 13, Defense = 7 },
                new Boss { Name = "Архимаг C++", Type = "Wizard", HP = 36, MaxHP = 36, Attack = 14, Defense = 2, FreezeChanceBonus = 10 },
                new Boss { Name = "Пестов С--", Type = "Skeleton", HP = 32, MaxHP = 32, Attack = 18, Defense = 3, FreezeChanceBonus = 15 }
        };
        return bosses[rand.Next(bosses.Length)];
    }

    private void Fight(Enemy enemy)
    {
        Console.WriteLine("Вы встретили врага!");
        while (enemy.HP > 0 && player.HP > 0)
        {
            if (player.IsFrozen)
            {
                Console.WriteLine("Вы заморожены и пропускаете ход!");
                player.IsFrozen = false;
            }
            else
            {
                Console.Write("Выберите действие (A - атаковать, D - защищаться): ");
                var action = Console.ReadKey().KeyChar.ToString().ToLower();
                Console.WriteLine();

                if (action == "a")
                {
                    int damage = player.GetTotalAttack();
                    enemy.HP -= damage;
                    Console.WriteLine($"Вы атаковали! Нанесли {damage} урона. У врага осталось {enemy.HP} HP.");
                }
                else if (action == "d")
                {
                    Console.WriteLine("Вы защищаетесь.");
                }
                if (enemy.HP <= 0)
                {
                    Console.WriteLine($"Вы победили {enemy.Name}!");
                    return;
                }

                // Ход врага
                int blockChance = rand.Next(100);
                bool isDodged = false;
                if (rand.Next(100) < 40) // 40% шанс уклониться
                {
                    Console.WriteLine("Вы уклонились от атаки!");
                    isDodged = true;
                }

                if (!isDodged)
                {
                    int damage = enemy.Attack;
                    if (enemy.Type == "Skeleton")
                    {
                        // Скелет игнорирует защиту
                    }
                    else
                    {
                        int block = rand.Next(70, 101) * player.GetTotalDefense() / 100;
                        damage = Math.Max(1, damage - block);
                    }

                    player.HP -= damage;
                    Console.WriteLine($"{enemy.Name} атакует! Вы получили {damage} урона.");
                }

                // Особенности врага
                if (enemy.Type == "Goblin")
                {
                    if (rand.Next(100) < 20) // 20% шанс крита
                    {
                        Console.WriteLine("Гоблин наносит критический удар!");
                        player.HP -= enemy.Attack;
                        Console.WriteLine($"Вы получили дополнительный урон! Осталось HP: {player.HP}");
                    }
                }
                else if (enemy.Type == "Mage")
                {
                    if (rand.Next(100) < 20) // 20% шанс заморозки
                    {
                        Console.WriteLine("Маг заморозил вас! Вы пропускаете следующий ход.");
                        player.IsFrozen = true;
                    }
                }

                if (player.HP <= 0)
                {
                    Console.WriteLine("Вы погибли...");
                    return;
                }
            }
        }
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