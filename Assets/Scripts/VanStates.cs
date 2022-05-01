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
            CheckConditions();
            return change;
        }
        else
        {
            tmp = waterTank;
            waterTank = 0;
            CheckConditions();
            return tmp;
        }
    }

    // produce grey tank, change positive
    public static void ProduceGrey(float change)
    {
        greyTank += change;
        CheckConditions();
    }

    // produce black
    public static void ProduceBlack(float change)
    {
        blackTank += change;
        CheckConditions();
    }

    public static void CheckConditions()
    {
        if (waterTank <= 0f)
            MyDialogManager.Show("The water container is empty.");
        if (greyTank >= 1f)
        {
            MyDialogManager.Show("The grey container is full. Will harm its condition.");
            // TODO: harm condition
        }
        if (blackTank >= 1f)
        {
            MyDialogManager.Show("The black container is full. Will harm its condition.");
            // TODO: harm condition
        }
    }
}
