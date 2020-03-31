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

    public event Action DayStarted;

    public float InGameSecondsPerRealtimeSeconds;
    [TextArea]
    public string DateTimeFormatString;

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
    }

    public void StartNewDay ()
    {
        DateTime = DateTime.AddDays(1);
        DateTime = DateTime.AddHours(DAY_START_HOUR - DAY_END_HOUR);
        Time.timeScale = 1;
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
