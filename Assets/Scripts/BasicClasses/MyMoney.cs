using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMoney
{
    public int money;

    public MyMoney(int m)
    {
        money = m;
    }

    public void Earn(int m)
    {
        money += m;
    }

    public void Spend(int m)
    {
        money -= m;
    }

    public bool Affordable(int m)
    {
        if (money >= m)
            return true;
        else
            return false;
    }

    public string GetStringShown()
    {
        return string.Format("¥{0}", money);
    }
}
