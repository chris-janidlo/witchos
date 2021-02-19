using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace WitchOS
{
    public class MirrorManager : MonoBehaviour
    {
        public List<Mirror> Mirrors;

        public IntVariable NumBrokenMirrors, NumIntactMirrors;

        public TimeState TimeState;

        void Start ()
        {
            TimeState.DayStarted.AddListener(resetMirrorStates);
        }

        void Update ()
        {
            foreach (var mirror in Mirrors)
            {
                mirror.Tick();
            }

            NumBrokenMirrors.Value = NumberInState(Mirror.State.Broken);
            NumIntactMirrors.Value = NumberInState(Mirror.State.Intact);
        }

        [ContextMenu("Try Deplete")]
        public void TryConsumeMagic ()
        {
            Mirror target = Mirrors
                .Where(m => m.CurrentState == Mirror.State.Broken)
                .OrderBy(m => m.DistanceFromSweetspot)
                .FirstOrDefault();

            if (target == null)
            {
                Debug.LogWarning("tried to consume when there were no mirrors to consume. ignoring that");
                return;
            }

            target.ConsumeMagic();
        }

        public int NumberInState (Mirror.State state)
        {
            return Mirrors.Count(m => m.CurrentState == state);
        }

        void resetMirrorStates ()
        {
            foreach (var mirror in Mirrors)
            {
                mirror.CurrentState = Mirror.State.Intact;
            }
        }
    }
}
