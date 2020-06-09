using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using crass;

namespace WitchOS
{
public class TimeState : Singleton<TimeState>
{
    // Friday the 13th in October, also happens to be a full moon, and on the brink of the millennium, pretty neat
    public static DateTime INITIAL_DATE = new DateTime(2000, 10, 13);

    // loop the calendar back to 2000 at the end of 2010, since 01/01/2000 is a Saturday and 12/31/2010 is the first Friday December 31st to occur afterward
    public static DateTime FINAL_DATE = new DateTime(2010, 12, 31);

    public DateTime DateTime
    {
        get => SaveManager.LooseSaveData.Value.Date;
        private set => SaveManager.LooseSaveData.Value.Date = value;
    }

    public UnityEvent DayStarted, DayEnded;

    void Awake ()
    {
        SingletonSetPersistantInstance(this);
    }

    void Start ()
    {
        StartNewDay();
    }

    [ContextMenu("End Day")]
    public void EndDay ()
    {
        DayEnded.Invoke();
        SaveManager.SaveAllData();
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
}
