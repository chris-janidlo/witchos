using UnityEngine;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Variable of type `Rite`. Inherits from `EquatableAtomVariable&lt;Rite, RitePair, RiteEvent, RitePairEvent, RiteRiteFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Rite", fileName = "RiteVariable")]
    public sealed class RiteVariable : EquatableAtomVariable<Rite, RitePair, RiteEvent, RitePairEvent, RiteRiteFunction> { }
}
