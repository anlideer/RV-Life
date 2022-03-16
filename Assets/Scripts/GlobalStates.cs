using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStates : MonoBehaviour
{
    public static MyTime currentTime = new MyTime(1, 8, 0);
    public static MyMoney currentMoney = new MyMoney(6000);
}
