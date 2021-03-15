using System;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference of type `SpellDeliverable`. Inherits from `AtomEventReference&lt;SpellDeliverable, SpellDeliverableVariable, SpellDeliverableEvent, SpellDeliverableVariableInstancer, SpellDeliverableEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class SpellDeliverableEventReference : AtomEventReference<
        SpellDeliverable,
        SpellDeliverableVariable,
        SpellDeliverableEvent,
        SpellDeliverableVariableInstancer,
        SpellDeliverableEventInstancer>, IGetEvent 
    { }
}
