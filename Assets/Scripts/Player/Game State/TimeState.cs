using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityAtoms.WitchOS;
using crass;

namespace WitchOS
{
    public class TimeState : Singleton<TimeState>
    {
        // Friday the 13th in October, also happens to be a full moon, and on the brink of the millennium, pretty neat
        public static readonly DateTime INITIAL_DATE = new DateTime(2000, 10, 13);

        // loop the calendar back to 2000 at the end of 2010, since 01/01/2000 is a Saturday and 12/31/2010 is the first Friday December 31st to occur afterward
        public static readonly DateTime FINAL_DATE = new DateTime(2010, 12, 31);

        // for consistent formatting across the game
        public static readonly CultureInfo CULTURE_INFO = CultureInfo.CreateSpecificCulture("en-US");

        public DateTime DateTime
        {
            get => DateTimeSaveData.Value.Value;
            private set => DateTimeSaveData.Value.Value = value;
        }

        public UnityEvent DayStarted, DayEnded;

        public SpellDeliverableValueList SpellEther;

        public DateTimeSaveData DateTimeSaveData;
        public SaveManager SaveManager;

        void Awake ()
        {
            SingletonSetPersistantInstance(this);
        }

        void Start ()
        {
            SaveManager.Register(DateTimeSaveData);

            StartNewDay();
        }

        [ContextMenu("End Day")]
        public void EndDay ()
        {
            DayEnded.Invoke();

            SpellEther.Clear();
            DateTime = AddDaysToToday(1);

            SaveManager.SaveAllData();
        }

        public void StartNewDay ()
        {
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

        public DateTime AddDaysToToday (int days)
        {
            var addedRaw = DateTime.AddDays(days).Date;

            return addedRaw.Date > FINAL_DATE.Date
                ? INITIAL_DATE.AddDays((addedRaw.Date - FINAL_DATE.Date).Days - 1)
                : addedRaw.Date;
        }
    }
}
