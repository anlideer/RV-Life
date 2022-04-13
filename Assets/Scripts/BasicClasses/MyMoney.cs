using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMoney
{
    public float money;

    public MyMoney(float m)
    {
        money = m;
    }

    public void Earn(float m)
    {
        money += m;
    }

    public void Spend(float m)
    {
        money -= m;
    }

    public bool Affordable(float m)
    {
        if (money >= m)
            return true;
        else
            return false;
    }

    public string GetStringShown()
    {
        return string.Format("¥{0}", (int)money);
    }
}
