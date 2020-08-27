using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using crass;

namespace WitchOS
{
    public class MirrorState : Singleton<MirrorState>
    {
        public enum State
        {
            Intact, Broken, Repairing, Dud
        }

        [Serializable]
        public class Mirror
        {
            public AnimationCurve RepairSweetspotCurve;
            public float MinimumRepairTime, MaximumRepairTime;

            public RuntimeAnimatorController AnimatorController;

            public State State;
            public float Timer;

            public float TimeUntilDud => RepairSweetspotCurve.keys[RepairSweetspotCurve.length - 1].time;
            public float DistanceFromSweetspot => RepairSweetspotCurve.Evaluate(Timer);
            public float RepairProgress => State == State.Repairing
                ? 1 - (Timer / currentRepairTime)
                : -1;

            float currentRepairTime;

            public void Break ()
            {
                if (State != State.Intact)
                {
                    throw new ArgumentException("can't break a mirror unless it's intact");
                }

                Timer = 0;
                State = State.Broken;
            }

            public void ConsumeMagic ()
            {
                if (State != State.Broken)
                {
                    throw new ArgumentException("can't consume a mirror unless it's broken");
                }

                Timer = Mathf.Lerp(MinimumRepairTime, MaximumRepairTime, DistanceFromSweetspot);
                currentRepairTime = Timer;
                State = State.Repairing;
            }

            public void Tick ()
            {
                switch (State)
                {
                    case State.Intact:
                    case State.Dud:
                        return;

                    case State.Broken:
                        Timer += Time.deltaTime;
                        if (Timer >= TimeUntilDud) State = State.Dud;
                        return;

                    case State.Repairing:
                        Timer -= Time.deltaTime;
                        if (Timer <= 0) State = State.Intact;
                        return;
                }
            }
        }

        public List<Mirror> Mirrors;

        void Awake ()
        {
            SingletonOverwriteInstance(this);
        }

        void Update ()
        {
            foreach (var mirror in Mirrors)
            {
                mirror.Tick();
            }
        }

        [ContextMenu("Try Deplete")]
        public bool TryConsumeMagic ()
        {
            Mirror target = Mirrors
                .Where(m => m.State == State.Broken)
                .OrderBy(m => m.DistanceFromSweetspot)
                .FirstOrDefault();

            if (target == null) return false;

            target.ConsumeMagic();
            return true;
        }

        public void ResetMirrorStates ()
        {
            foreach (var mirror in Mirrors)
            {
                mirror.State = State.Intact;
            }
        }

        public int NumberIntact ()
        {
            return Mirrors.Where(m => m.State == State.Intact).Count();
        }

        public int NumberBroken ()
        {
            return Mirrors.Where(m => m.State == State.Broken).Count();
        }
    }
}
