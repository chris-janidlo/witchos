using UnityEngine;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Variable of type `SpellDeliverable`. Inherits from `EquatableAtomVariable&lt;SpellDeliverable, SpellDeliverablePair, SpellDeliverableEvent, SpellDeliverablePairEvent, SpellDeliverableSpellDeliverableFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/SpellDeliverable", fileName = "SpellDeliverableVariable")]
    public sealed class SpellDeliverableVariable : EquatableAtomVariable<SpellDeliverable, SpellDeliverablePair, SpellDeliverableEvent, SpellDeliverablePairEvent, SpellDeliverableSpellDeliverableFunction> { }
}
