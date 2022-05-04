using System.Collections.Generic;
using UnityEngine;

public enum WeatherType
{
    NORMAL, RAIN, RAINSTORM,
}

public class Weather
{
    public WeatherType weather = WeatherType.NORMAL;
    public int duration;
    public int startHour;

    // next day
    public WeatherType nextDayWeather;
    // if next day normal, then ignore these below
    public int nextDuration;
    public int nextStartHour;
    
    public WeatherType GetWeatherNow()
    {
        if (weather == WeatherType.NORMAL)
            return weather;
        else
        {
            if (GlobalStates.currentTime.hour >= startHour && GlobalStates.currentTime.hour < duration + startHour)
                return weather;
            else
                return WeatherType.NORMAL;
        }
    }

    public void GenerateNextDay()
    {
        // assign today
        // if not the first day
        if (GlobalStates.currentTime.day != 1)
        {
            weather = nextDayWeather;
            duration = nextDuration;
            startHour = nextStartHour;
        }

        // rain?
        float r = Random.value;
        if (r < 0.8f)
        {
            nextDayWeather = WeatherType.NORMAL;
            nextDuration = 0;
            nextStartHour = 0;
        }
        else if (r < 0.95f)
        {
            nextDayWeather = WeatherType.RAIN;
            nextDuration = Random.Range(2, 10);
            nextStartHour = Random.Range(1, 23 - nextDuration);
        }
        else
        {
            nextDayWeather = WeatherType.RAINSTORM;
            nextDuration = Random.Range(2, 5);
            nextStartHour = Random.Range(1, 23 - nextDuration);
        }
    }

    public string GetForecast()
    {
        float r = Random.value;
        // correct
        if (r < 0.9f)
        {
            if (nextDayWeather == WeatherType.NORMAL)
                return "Next day will not rain.";
            else if (nextDayWeather == WeatherType.RAIN)
                return "Next day will rain.";
            else
                return "Next day will be rainstorm!";
        }
        // wrong
        else
        {
            if (nextDayWeather == WeatherType.NORMAL)
                return "Next day will rain.";
            else
                return "Next day will not rain.";
        }
    }
}