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
    [CreateAssetMenu(menuName = "WitchOS/Time State", fileName = "newTimeState.asset")]
    public class TimeState : InitializableSO
    {
        // for consistent formatting across the game
        public static readonly CultureInfo CULTURE_INFO = CultureInfo.CreateSpecificCulture("en-US");

        public DateTime DateTime
        {
            get => DateTimeSaveData.Value.Value;
            private set => DateTimeSaveData.Value.Value = value;
        }

        // Friday the 13th in October, also happens to be a full moon, and on the brink of the millennium, pretty neat
        [SerializeField]
        SaveableDate _initialDate = new SaveableDate(new DateTime(2000, 10, 13));
        public DateTime InitialDate => _initialDate.Value;

        // loop the calendar back to 2000 at the end of 2010, since 01/01/2000 is a Saturday and 12/31/2010 is the first Friday December 31st to occur afterward
        [SerializeField]
        SaveableDate _finalDate = new SaveableDate(new DateTime(2010, 12, 31));
        public DateTime FinalDate => _finalDate.Value;

        public UnityEvent DayStarted, DayEnded;

        public DateTimeSaveData DateTimeSaveData;
        public SaveManager SaveManager;

        public override void Initialize ()
        {
            SaveManager.Register(DateTimeSaveData);
            StartNewDay();
        }

        [ContextMenu("End Day")]
        public void EndDay ()
        {
            DayEnded.Invoke();
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
            int daysElapsed = (DateTime.Date - InitialDate.Date).Days;
            return (MoonPhase) (daysElapsed % EnumUtil.NameCount<MoonPhase>());
        }

        // yeah yeah smelly I know
        public MoonPhase GetTomorrowsMoonPhase ()
        {
            int daysElapsed = (DateTime.Date == FinalDate.Date)
                ? 1
                : (DateTime.Date - InitialDate.Date).Days + 1;

            return (MoonPhase) (daysElapsed % EnumUtil.NameCount<MoonPhase>());
        }

        public DateTime AddDaysToToday (int days)
        {
            var addedRaw = DateTime.AddDays(days).Date;

            return addedRaw.Date > FinalDate.Date
                ? InitialDate.AddDays((addedRaw.Date - FinalDate.Date).Days - 1)
                : addedRaw.Date;
        }

        public string PrettyFormatDate (DateTime date)
        {
            return date.ToString(CULTURE_INFO);
        }
    }
}
