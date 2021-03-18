using UnityEngine;
using UnityAtoms.BaseAtoms;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Variable Instancer of type `Rite`. Inherits from `AtomVariableInstancer&lt;RiteVariable, RitePair, Rite, RiteEvent, RitePairEvent, RiteRiteFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/Rite Variable Instancer")]
    public class RiteVariableInstancer : AtomVariableInstancer<
        RiteVariable,
        RitePair,
        Rite,
        RiteEvent,
        RitePairEvent,
        RiteRiteFunction>
    { }
}
