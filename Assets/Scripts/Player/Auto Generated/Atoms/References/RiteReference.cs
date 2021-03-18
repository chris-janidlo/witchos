using System;
using UnityAtoms.BaseAtoms;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Reference of type `Rite`. Inherits from `EquatableAtomReference&lt;Rite, RitePair, RiteConstant, RiteVariable, RiteEvent, RitePairEvent, RiteRiteFunction, RiteVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class RiteReference : EquatableAtomReference<
        Rite,
        RitePair,
        RiteConstant,
        RiteVariable,
        RiteEvent,
        RitePairEvent,
        RiteRiteFunction,
        RiteVariableInstancer>, IEquatable<RiteReference>
    {
        public RiteReference() : base() { }
        public RiteReference(Rite value) : base(value) { }
        public bool Equals(RiteReference other) { return base.Equals(other); }
    }
}
