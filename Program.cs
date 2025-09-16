using System;
using System.ComponentModel.DataAnnotations;

class Store
{
    public int ID;
    public string Name = "Product";
    public int Price;
    public int Quantity;
    public enum Availability
    {
        None, // 0
        One, // 1
        MoreThanOne //2+
    };

    public void Add()
    {

    }

    public void Delete()
    {

    }

    public void Order()
    {

    }

    public void Sell()
    {

    }

    public void Research()
    {

    }
}