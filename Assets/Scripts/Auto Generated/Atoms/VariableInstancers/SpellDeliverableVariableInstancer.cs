using UnityEngine;
using UnityAtoms.BaseAtoms;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Variable Instancer of type `SpellDeliverable`. Inherits from `AtomVariableInstancer&lt;SpellDeliverableVariable, SpellDeliverablePair, SpellDeliverable, SpellDeliverableEvent, SpellDeliverablePairEvent, SpellDeliverableSpellDeliverableFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/SpellDeliverable Variable Instancer")]
    public class SpellDeliverableVariableInstancer : AtomVariableInstancer<
        SpellDeliverableVariable,
        SpellDeliverablePair,
        SpellDeliverable,
        SpellDeliverableEvent,
        SpellDeliverablePairEvent,
        SpellDeliverableSpellDeliverableFunction>
    { }
}
