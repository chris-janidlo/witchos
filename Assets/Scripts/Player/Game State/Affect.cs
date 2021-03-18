using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    [CreateAssetMenu(menuName = "WitchOS/Affect", fileName = "newAffecct.asset")]
    public class Affect : ScriptableObject
    {
        [Tooltip("The rites that produce this affect when performed")]
        public List<Rite> ProducingRites;
    }
}
