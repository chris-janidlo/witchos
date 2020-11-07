using System;
using UnityAtoms.BaseAtoms;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Reference of type `SpellDeliverable`. Inherits from `EquatableAtomReference&lt;SpellDeliverable, SpellDeliverablePair, SpellDeliverableConstant, SpellDeliverableVariable, SpellDeliverableEvent, SpellDeliverablePairEvent, SpellDeliverableSpellDeliverableFunction, SpellDeliverableVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class SpellDeliverableReference : EquatableAtomReference<
        SpellDeliverable,
        SpellDeliverablePair,
        SpellDeliverableConstant,
        SpellDeliverableVariable,
        SpellDeliverableEvent,
        SpellDeliverablePairEvent,
        SpellDeliverableSpellDeliverableFunction,
        SpellDeliverableVariableInstancer>, IEquatable<SpellDeliverableReference>
    {
        public SpellDeliverableReference() : base() { }
        public SpellDeliverableReference(SpellDeliverable value) : base(value) { }
        public bool Equals(SpellDeliverableReference other) { return base.Equals(other); }
    }
}
