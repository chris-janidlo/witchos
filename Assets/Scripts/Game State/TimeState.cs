using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using crass;

public class TimeState : Singleton<TimeState>
{
    // Friday the 13th in October, also happens to be a full moon, and on the brink of the millennium, pretty neat
    public static DateTime INITIAL_DATE = new DateTime(2000, 10, 13);

    // loop the calendar back to 2000 when we reach the year 2028, since those two years share the same calendar
    public static DateTime FINAL_DATE = new DateTime(2027, 12, 31);

    public DateTime DateTime { get; private set; }

    public UnityEvent DayStarted, DayEnded;

    void Awake ()
    {
        SingletonSetPersistantInstance(this);
    }

    void Start ()
    {
        DateTime = INITIAL_DATE.AddDays(-1); // since StartNewDay will increment by 1
        StartNewDay();
    }

    [ContextMenu("End Day")]
    public void EndDay ()
    {
        DayEnded.Invoke();
    }

    public void StartNewDay ()
    {
        DateTime = (DateTime.Date == FINAL_DATE.Date)
            ? INITIAL_DATE
            : DateTime.AddDays(1);

        DayStarted.Invoke();
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
        int daysElapsed = (DateTime.Date == FINAL_DATE.Date)
            ? 1
            : (DateTime.Date - INITIAL_DATE.Date).Days + 1;

        return (MoonPhase) (daysElapsed % EnumUtil.NameCount<MoonPhase>());
    }
}
