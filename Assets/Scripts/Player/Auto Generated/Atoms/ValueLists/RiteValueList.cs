using UnityEngine;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Value List of type `Rite`. Inherits from `AtomValueList&lt;Rite, RiteEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/Rite", fileName = "RiteValueList")]
    public sealed class RiteValueList : AtomValueList<Rite, RiteEvent> { }
}
