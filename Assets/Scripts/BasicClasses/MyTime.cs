using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTime
{
    public int day;
    public int hour;
    public int minute;
    private float minuteToBeAdded = 0;

    public MyTime(int d, int h, int m)
    {
        day = d;
        hour = h;
        minute = m;
    }

    public void TimePass(MyTime t)
    {
        day += t.day;
        hour += t.hour;
        minute += t.minute;
        if (minute >= 60)
        {
            hour += minute / 60;
            minute = minute % 60;
        }
        if (hour >= 24)
        {
            day += hour / 24;
            hour = hour % 24;
        }
    }

    public void TimePassLittle(float m)
    {
        minuteToBeAdded += m;
        if (minuteToBeAdded >= 1f)
        {
            TimePass(new MyTime(0, 0, (int)(minuteToBeAdded)));
            minuteToBeAdded -= (int)(minuteToBeAdded);
        }
    }

    public string GetStringShown()
    {
        string s = string.Format("Day {0}\n{1}:{2:2}", day, hour.ToString("00"), minute.ToString("00"));
        return s;
    }
}
