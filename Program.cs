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
public class HealPotion
{
    public HealPotion() { }

    public  void FullHeal()
    {
        Player.hp = 100;
    } 
}
//Сундук
public class Chest
{
    public string HealPotion { get; set; }

}
//Основной код игры
public class Game
{

}