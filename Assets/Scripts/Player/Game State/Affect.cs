using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    [CreateAssetMenu(menuName = "WitchOS/Affect", fileName = "newAffecct.asset")]
    public class Affect : ScriptableObject
    {
        /// <summary>
        /// The human-friendly name of the affect this object represents, as opposed to <see cref="name"/>, which is the name of the object itself.
        /// </summary>
        public string Name;

        [Tooltip("The rites that produce this affect when performed")]
        public List<Rite> ProducingRites;
    }
}
