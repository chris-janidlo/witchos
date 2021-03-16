using UnityEngine;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Value List of type `SpellDeliverable`. Inherits from `AtomValueList&lt;SpellDeliverable, SpellDeliverableEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/SpellDeliverable", fileName = "SpellDeliverableValueList")]
    public sealed class SpellDeliverableValueList : AtomValueList<SpellDeliverable, SpellDeliverableEvent> { }
}
