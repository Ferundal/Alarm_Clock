using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class ClockTime
{
    public static ClockTime CurrentTime { get; private set; }
    private int b_Seconds;
    public int Seconds
    {
        get
        {
            return b_Seconds;
        }
        set
        {
            if (value < 0)
            {
                value %= 60;
                value += 60;
            }
            b_Seconds = value % 60;
        }
    }
    private int b_Minutes;
    public int Minutes
    {
        get
        {
            return b_Minutes;
        }
        set
        {
            if (value < 0)
            {
                value %= 60;
                value += 60;
            }
            b_Minutes = value % 60;
        }
    }
    private int b_Hours;
    public int Hours
    {
        get
        {
            return b_Hours;
        }
        set
        {
            if (value < 0)
            {
                value %= 24;
                value += 24;
            }
            b_Hours = value % 24;
        }
    }

    private ClockTime() {}
    public ClockTime(int hours, int minutes, int seconds)
    {
        this.Hours = hours;
        this.Minutes = minutes;
        this.Seconds = seconds;
    }

    public ClockTime(ClockTime clockTime)
    {
        this.Seconds = clockTime.Seconds;
        this.Minutes = clockTime.Minutes;
        this.Hours = clockTime.Hours;
    }

    public void AddSecond()
    {
        ++b_Seconds;
        b_Minutes += b_Seconds / 60;
        b_Seconds %= 60;
        b_Hours += b_Minutes / 60;
        b_Minutes %= 60;
        b_Hours %= 24;
    }

    private class WorldTimeAPI
    {
        public string datetime;
    }
    public static ClockTime ParseWorldTimeAPI(string worldTimeAPI_String)
    {
        WorldTimeAPI worldTimeAPI = JsonUtility.FromJson<WorldTimeAPI>(worldTimeAPI_String);
        string time = Regex.Match(worldTimeAPI.datetime, @"\d{2}:\d{2}:\d{2}").Value;
        string[] splitedTime = time.Split(':');
        return new ClockTime(int.Parse(splitedTime[0]), int.Parse(splitedTime[1]), int.Parse(splitedTime[2]));
    }

    private class WorldClockAPI
    {
        public string currentDateTime;
    }
    public static ClockTime ParseWorldClockAPI(string worldTimeAPI_String)
    {
        WorldClockAPI worldTimeAPI = JsonUtility.FromJson<WorldClockAPI>(worldTimeAPI_String);
        string time = Regex.Match(worldTimeAPI.currentDateTime, @"\d{2}:\d{2}").Value;
        string[] splitedTime = time.Split(':');
        return new ClockTime(int.Parse(splitedTime[0]), int.Parse(splitedTime[1]), 0);
    }

    public bool Equals(ClockTime obj)
    {
        if (b_Seconds == obj.b_Seconds
            && b_Minutes == obj.b_Minutes
            && b_Hours == obj.b_Hours)
        {
            return true;
        }
        return false;
    }
}
