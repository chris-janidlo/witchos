using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class TimeState : Singleton<TimeState>
{
    // Friday the 13th in October, also happens to be a full moon, and on the brink of the millennium, pretty neat
    public static DateTime INITIAL_DATE = new DateTime(2000, 10, 13);

    public static int
        DAY_START_HOUR = 18,
        DAY_END_HOUR = 23;

    public DateTime DateTime { get; private set; } = INITIAL_DATE.AddHours(DAY_START_HOUR);

    public event Action DayStarted, HourStarted;

    public float InGameSecondsPerRealtimeSeconds;
    [TextArea]
    public string DateTimeFormatString;

    public string TimeWarningOneText, TimeWarningTwoText;
    public int MinutesBeforeEndOfDayForWarningOne, MinutesBeforeEndOfDayForWarningTwo;

    int hourTicker;
    bool warnedOnce, warnedTwice;

    void Awake ()
    {
        SingletonSetPersistantInstance(this);
    }

    void Update ()
    {
        DateTime = DateTime.AddSeconds(InGameSecondsPerRealtimeSeconds * Time.deltaTime);

        // TODO: add warning before abruptly ending
        if (DateTime.Hour >= DAY_END_HOUR)
        {
            Time.timeScale = 0;
            DaySummaryScreen.Instance.ShowSummary();
        }

        if (DateTime.Hour > hourTicker)
        {
            hourTicker = DateTime.Hour;
            HourStarted?.Invoke();
        }

        DateTime endOfDay = new DateTime
        (
            DateTime.Year,
            DateTime.Month,
            DateTime.Day,
            DAY_END_HOUR,
            0,
            0
        );

        if (!warnedOnce && (endOfDay - DateTime).TotalMinutes <= MinutesBeforeEndOfDayForWarningOne)
        {
            warnedOnce = true;
            Alert.Instance.ShowMessageNext(TimeWarningOneText);
        }

        if (!warnedTwice && (endOfDay - DateTime).TotalMinutes <= MinutesBeforeEndOfDayForWarningTwo)
        {
            warnedTwice = true;
            Alert.Instance.ShowMessageNext(TimeWarningTwoText);
        }
    }

    public void StartNewDay ()
    {
        DateTime = DateTime.AddDays(1);
        DateTime = DateTime.AddHours(DAY_START_HOUR - DAY_END_HOUR);
        Time.timeScale = 1;

        hourTicker = 0;
        warnedOnce = false;
        warnedTwice = false;
    
        DayStarted?.Invoke();
    }

    public string GetTimeString ()
    {
        return DateTime.ToString(DateTimeFormatString, CultureInfo.CreateSpecificCulture("en-US"));
    }

    // yeah yeah smelly I know
    public MoonPhase GetTodaysMoonPhase ()
    {
        int daysElapsed = (DateTime.Date - INITIAL_DATE.Date).Days;
        return (MoonPhase) (daysElapsed % EnumUtil.NameCount<MoonPhase>());
    }

    // yeah yeah smelly I know
    public MoonPhase GetTomorrowsMoonPhase ()
    {
        int daysElapsed = (DateTime.Date - INITIAL_DATE.Date).Days + 1;
        return (MoonPhase) (daysElapsed % EnumUtil.NameCount<MoonPhase>());
    }
}
