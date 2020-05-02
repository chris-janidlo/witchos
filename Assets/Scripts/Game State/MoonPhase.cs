using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoonPhase
{
    FullMoon, WaningGibbous, ThirdQuarter, WaningCrescent, NewMoon, WaxingCrescent, FirstQuarter, WaxingGibbous
}

public enum MoonPhaseChange
{
    Waxing, Waning, Neither
}

public static class MoonPhaseExtensions
{
    public static string ToString (this MoonPhase phase, bool includeWaxWane)
    {
        switch (phase)
        {
            case MoonPhase.FullMoon:        return "Full Moon";
            case MoonPhase.WaningGibbous:   return (includeWaxWane ? "Waning " : "") + "Gibbous";
            case MoonPhase.ThirdQuarter:    return "Third Quarter";
            case MoonPhase.WaningCrescent:  return (includeWaxWane ? "Waning " : "") + "Crescent";
            case MoonPhase.NewMoon:         return "New Moon";
            case MoonPhase.WaxingCrescent:  return (includeWaxWane ? "Waxing " : "") + "Crescent";
            case MoonPhase.FirstQuarter:    return "First Quarter";
            case MoonPhase.WaxingGibbous:   return (includeWaxWane ? "Waxing " : "") + "Gibbous";

            default: throw new ArgumentException($"unexpected MoonPhase {phase}");
        }
    }

    public static MoonPhaseChange GetPhaseChange (this MoonPhase phase)
    {
        switch (phase)
        {
            case MoonPhase.FullMoon:        return MoonPhaseChange.Neither;
            case MoonPhase.WaningGibbous:   return MoonPhaseChange.Waning;
            case MoonPhase.ThirdQuarter:    return MoonPhaseChange.Waning;
            case MoonPhase.WaningCrescent:  return MoonPhaseChange.Waning;
            case MoonPhase.NewMoon:         return MoonPhaseChange.Neither;
            case MoonPhase.WaxingCrescent:  return MoonPhaseChange.Waxing;
            case MoonPhase.FirstQuarter:    return MoonPhaseChange.Waxing;
            case MoonPhase.WaxingGibbous:   return MoonPhaseChange.Waxing;

            default: throw new ArgumentException($"unexpected MoonPhase {phase}");
        }
    }
}
