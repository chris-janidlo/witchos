using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class TimeState : Singleton<TimeState>
{
    // Friday the 13th in October, also happens to be a full moon, and on the brink of the millennium, pretty neat
    public static DateTime InitialDateTime = new DateTime(2000, 10, 13, 18, 0, 0);

    public DateTime DateTime { get; private set; }

    public float InGameSecondsPerRealtimeSeconds;
    [TextArea]
    public string DateTimeFormatString;

    void Awake ()
    {
        SingletonSetPersistantInstance(this);

        DateTime = InitialDateTime;
    }

    void Update ()
    {
        DateTime = DateTime.AddSeconds(InGameSecondsPerRealtimeSeconds * Time.deltaTime);
    }

    public string GetTimeString ()
    {
        return DateTime.ToString(DateTimeFormatString, CultureInfo.CreateSpecificCulture("en-US"));
    }

    // yeah yeah smelly I know
    public MoonPhase GetTodaysMoonPhase ()
    {
        int daysElapsed = (DateTime.Date - InitialDateTime.Date).Days;
        return (MoonPhase) (daysElapsed % EnumUtil.NameCount<MoonPhase>());
    }

    // yeah yeah smelly I know
    public MoonPhase GetTomorrowsMoonPhase ()
    {
        int daysElapsed = (DateTime.Date - InitialDateTime.Date).Days + 1;
        return (MoonPhase) (daysElapsed % EnumUtil.NameCount<MoonPhase>());
    }
}
