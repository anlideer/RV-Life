using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyTime
{
    public int day;
    public int hour;
    public int minute;
    private float minuteToBeAdded = 0;
    private bool reported = false;  // rain reported

    public MyTime(int d, int h, int m)
    {
        day = d;
        hour = h;
        minute = m;
    }

    public void TimePass(MyTime t)
    {
        int lastDay = day;
        int lastHour = hour;
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

        TimePassPenalty(t);
        if (day > lastDay)
        {
            GlobalStates.currentWeather.GenerateNextDay();
            reported = false;
            Debug.Log(GlobalStates.currentWeather.nextDayWeather);
            Debug.Log(GlobalStates.currentWeather.nextStartHour);
            Debug.Log(GlobalStates.currentWeather.nextDuration);
        }

        var w = GlobalStates.currentWeather;
        if (hour > lastHour && w.weather != WeatherType.NORMAL)
        {
            if (reported)
            {
                if (hour >= w.startHour + w.duration)
                {
                    MyDialogManager.Show("The rain stopped. The weather is now normal again.");
                    reported = false;
                }
            }
            else
            {
                if (hour >= w.startHour && hour < w.startHour + w.duration)
                {
                    reported = true;
                    string description;
                    if (w.weather == WeatherType.RAIN)
                        description = "Rain started. The speed will be slightly lower.";
                    else
                        description = "Rainstorm started. It's better not to drive.";
                    MyDialogManager.Show(description);
                }
            }
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

    // return the hour pass
    public float TimePassUntil(MyTime t)
    {
        int d = t.day - day;
        int h = t.hour - hour;
        int m = t.minute - minute;
        if (m < 0)
        {
            m += 60;
            h -= 1;
        }
        if (h < 0)
        {
            h += 24;
            d -= 1;
        }
        if (d < 0)
        {
            Debug.LogWarning("Invalid time pass until");
            return 0f;
        }

        TimePass(new MyTime(d, h, m));
        return d * 24 + h + m / 60;
    }

    public string GetStringShown()
    {
        string s = string.Format("Day {0}\n{1}:{2:2}", day, hour.ToString("00"), minute.ToString("00"));
        return s;
    }

    // when time pass, some data goes down (health, cleanness)
    private void TimePassPenalty(MyTime t)
    {
        int tmpMinutes = t.minute;
        tmpMinutes += 60 * t.hour;
        tmpMinutes += 24 * 60 * t.day;
        
        if (!GlobalStates.isSleeping)
        {
            VanStates.ConsumeWater(0.00006f * tmpMinutes);
            VanStates.ProduceBlack(0.00005f * tmpMinutes);
            VanStates.ProduceGrey(0.00005f * tmpMinutes);
            GlobalStates.ChangeHealth(-0.0012f * tmpMinutes);
            GlobalStates.ChangeClean(-0.00035f * tmpMinutes);
            GlobalStates.ChangeEnergy(-0.001f * tmpMinutes);
        }
        else
        {
            GlobalStates.ChangeHealth(-0.0008f * tmpMinutes);
            GlobalStates.ChangeClean(-0.00025f * tmpMinutes);
        }

        if (!GlobalStates.isDriving)
        {
            GlobalStates.ChangeBattery(-0.0002f * tmpMinutes);
        }
    }

}
