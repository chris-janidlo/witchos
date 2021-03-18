using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    [CreateAssetMenu(menuName = "WitchOS/Ritual", fileName = "newRitual.asset")]
    public class Ritual : ScriptableObject
    {
        /// <summary>
        /// The human-friendly name of the ritual this object represents, as opposed to <see cref="name"/>, which is the name of the object itself.
        /// </summary>
        public string Name;

        [Tooltip("The emotion this ritual produces when rites that produce the proper affects are performed")]
        public Emotion Product;

        [Tooltip("The player must perform rites that produce these affects in the order they are listed here in order for this ritual to produce its Product")]
        public List<Affect> RequiredAffects;
    }
}
