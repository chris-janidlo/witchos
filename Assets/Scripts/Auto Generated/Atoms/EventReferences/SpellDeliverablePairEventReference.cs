using System;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference of type `SpellDeliverablePair`. Inherits from `AtomEventReference&lt;SpellDeliverablePair, SpellDeliverableVariable, SpellDeliverablePairEvent, SpellDeliverableVariableInstancer, SpellDeliverablePairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class SpellDeliverablePairEventReference : AtomEventReference<
        SpellDeliverablePair,
        SpellDeliverableVariable,
        SpellDeliverablePairEvent,
        SpellDeliverableVariableInstancer,
        SpellDeliverablePairEventInstancer>, IGetEvent 
    { }
}
