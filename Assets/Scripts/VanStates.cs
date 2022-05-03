using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanStates : MonoBehaviour
{
    public static float waterTank = 1f;
    public static float greyTank = 0f;
    public static float blackTank = 0f;
    public static float greyCondition = 1f;
    public static float blackCondition = 1f;


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

        // condition
        float fac = 0.01f;
        if (greyTank >= 1f)
            fac = 0.2f;
        greyCondition -= change * fac;
    }

    // produce black
    public static void ProduceBlack(float change)
    {
        blackTank += change;
        CheckConditions();

        // condition
        float fac = 0.01f;
        if (blackTank >= 1f)
            fac = 0.2f;
        blackCondition -= change * fac;

    }

    public static void CheckConditions()
    {
        if (waterTank <= 0f)
            MyDialogManager.Show("The water container is empty.");
        if (greyTank >= 1f)
        {
            MyDialogManager.Show("The grey container is full. Will harm its condition.");
        }
        if (blackTank >= 1f)
        {
            MyDialogManager.Show("The black container is full. Will harm its condition.");
        }
        // conditions
        if (greyCondition <= 0f)
        {
            if (GlobalStates.currentMoney.Affordable(500))
            {
                MyDialogManager.Show("The grey container is entirely broken. Call for help... (Cost 500)");
                GlobalStates.currentTime.TimePass(new MyTime(0, 4, 0));
                GlobalStates.currentMoney.Spend(500);
                greyCondition = 1f;
                greyTank = 0f;
            }
            else
            {
                MyDialogManager.ShowandGameover(new List<string>
                {
                    "The grey container is entirely broken. You can't afford to call for help (cost 500).",
                    "Game Over"
                });
            }
        }

        if (blackCondition <= 0f)
        {
            if (GlobalStates.currentMoney.Affordable(500))
            {
                MyDialogManager.Show("The black container is entirely broken. Call for help... (Cost 500)");
                GlobalStates.currentTime.TimePass(new MyTime(0, 4, 0));
                GlobalStates.currentMoney.Spend(500);
                blackCondition = 1f;
                blackTank = 0f;
            }
            else
            {
                MyDialogManager.ShowandGameover(new List<string>
                {
                    "The black container is entirely broken. You can't afford to call for help (cost 500).",
                    "Game Over"
                });
            }
        }
    }

    public static void MaintainSystem()
    {
        float m = 0.2f;
        greyCondition += m;
        blackCondition += m;
        CheckConditions();
        List<string> res = new List<string>();
        if (greyCondition < 0.3f)
            res.Add("The grey container is in poor condition");
        if (blackCondition < 0.3f)
            res.Add("The black container is in poor condition");
        if (res.Count > 0)
            MyDialogManager.Show(res);
    }
}
