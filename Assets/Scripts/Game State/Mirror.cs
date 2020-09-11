using System;
using UnityEngine;

namespace WitchOS
{
    [CreateAssetMenu(menuName = "WitchOS/Mirror State", fileName = "New Mirror.asset")]
    public class Mirror : ScriptableObject
    {
        public enum State
        {
            Intact, Broken, Repairing, Dud
        }

        public AnimationCurve RepairSweetspotCurve;
        public float MinimumRepairTime, MaximumRepairTime;

        public RuntimeAnimatorController AnimatorController;

        [NonSerialized]
        public State CurrentState;
        [NonSerialized]
        public float Timer;

        public float TimeUntilDud => RepairSweetspotCurve.keys[RepairSweetspotCurve.length - 1].time;
        public float DistanceFromSweetspot => RepairSweetspotCurve.Evaluate(Timer);
        public float RepairProgress => CurrentState == State.Repairing
            ? 1 - (Timer / currentRepairTime)
            : -1;

        float currentRepairTime;

        public void Break ()
        {
            if (CurrentState != State.Intact)
            {
                throw new ArgumentException("can't break a mirror unless it's intact");
            }

            Timer = 0;
            CurrentState = State.Broken;
        }

        public void ConsumeMagic ()
        {
            if (CurrentState != State.Broken)
            {
                throw new ArgumentException("can't consume a mirror unless it's broken");
            }

            Timer = Mathf.Lerp(MinimumRepairTime, MaximumRepairTime, DistanceFromSweetspot);
            currentRepairTime = Timer;
            CurrentState = State.Repairing;
        }

        public void Tick ()
        {
            switch (CurrentState)
            {
                case State.Intact:
                case State.Dud:
                    return;

                case State.Broken:
                    Timer += Time.deltaTime;
                    if (Timer >= TimeUntilDud) CurrentState = State.Dud;
                    return;

                case State.Repairing:
                    Timer -= Time.deltaTime;
                    if (Timer <= 0) CurrentState = State.Intact;
                    return;
            }
        }
    }
}
