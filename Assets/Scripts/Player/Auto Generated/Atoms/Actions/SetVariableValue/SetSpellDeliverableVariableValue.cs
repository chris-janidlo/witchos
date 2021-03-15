using UnityEngine;
using UnityAtoms.BaseAtoms;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Set variable value Action of type `SpellDeliverable`. Inherits from `SetVariableValue&lt;SpellDeliverable, SpellDeliverablePair, SpellDeliverableVariable, SpellDeliverableConstant, SpellDeliverableReference, SpellDeliverableEvent, SpellDeliverablePairEvent, SpellDeliverableVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/SpellDeliverable", fileName = "SetSpellDeliverableVariableValue")]
    public sealed class SetSpellDeliverableVariableValue : SetVariableValue<
        SpellDeliverable,
        SpellDeliverablePair,
        SpellDeliverableVariable,
        SpellDeliverableConstant,
        SpellDeliverableReference,
        SpellDeliverableEvent,
        SpellDeliverablePairEvent,
        SpellDeliverableSpellDeliverableFunction,
        SpellDeliverableVariableInstancer>
    { }
}
