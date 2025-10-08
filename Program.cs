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

}
class Program
{
    static void Main(string[] args)
    {
        var game = new Game();
        game.Start();
    }
}