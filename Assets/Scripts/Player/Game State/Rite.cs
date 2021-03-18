using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    [CreateAssetMenu(menuName = "WitchOS/Rite", fileName = "newRite.asset")]
    public class Rite : ScriptableObject
    {
        /// <summary>
        /// The human-friendly name of the rite this object represents, as opposed to <see cref="name"/>, which is the name of the object itself.
        /// </summary>
        public string Name;

        [TextArea(5, 500)]
        public string Description;
    }
}
