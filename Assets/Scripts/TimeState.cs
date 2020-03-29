using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class TimeState : Singleton<TimeState>
{
    public DateTime DateTime { get; private set; }

    public float InGameSecondsPerRealtimeSeconds;
    [TextArea]
    public string DateTimeFormatString;

    void Awake ()
    {
        SingletonSetPersistantInstance(this);

        DateTime = new DateTime(2020, 3, 13, 18, 0, 0);
    }

    void Update ()
    {
        DateTime = DateTime.AddSeconds(InGameSecondsPerRealtimeSeconds * Time.deltaTime);
    }

    public string GetTimeString ()
    {
        return DateTime.ToString(DateTimeFormatString, CultureInfo.CreateSpecificCulture("en-US"));
    }
}
