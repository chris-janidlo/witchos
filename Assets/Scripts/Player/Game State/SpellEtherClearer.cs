using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.WitchOS;

namespace WitchOS
{
    public class SpellEtherClearer : MonoBehaviour
    {
        public SpellDeliverableValueList SpellEther;

        void Awake ()
        {
            SpellEther.Clear();
        }

        public void OnDayEnded ()
        {
            SpellEther.Clear();
        }
    }
}
