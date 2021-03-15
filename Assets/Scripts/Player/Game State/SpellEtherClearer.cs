using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.WitchOS;

namespace WitchOS
{
    public class SpellEtherClearer : MonoBehaviour
    {
        public SpellDeliverableValueList SpellEther;

        public TimeState TimeState;

        void Awake ()
        {
            SpellEther.Clear();
            TimeState.DayEnded.AddListener(SpellEther.Clear);
        }
    }
}
