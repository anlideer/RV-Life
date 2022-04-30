using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanStates : MonoBehaviour
{
    public static float waterTank = 1f;
    public static float greyTank = 0f;
    public static float blackTank = 0f;


    // change must be positive
    // return the actual amount consumed
    public static float ConsumeWater(float change)
    {
        float tmp = waterTank - change;
        if (tmp >= 0f)
        {
            waterTank = tmp;
            return change;
        }
        else
        {
            tmp = waterTank;
            waterTank = 0;
            return tmp;
        }
    }

    // produce grey tank, change positive
    public static void ProduceGrey(float change)
    {
        greyTank += change;
    }

    // produce black
    public static void ProduceBlack(float change)
    {
        blackTank += change;
    }
}
