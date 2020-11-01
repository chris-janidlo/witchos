using System;
using UnityAtoms.BaseAtoms;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Reference of type `Order`. Inherits from `EquatableAtomReference&lt;Order, OrderPair, OrderConstant, OrderVariable, OrderEvent, OrderPairEvent, OrderOrderFunction, OrderVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class OrderReference : EquatableAtomReference<
        Order,
        OrderPair,
        OrderConstant,
        OrderVariable,
        OrderEvent,
        OrderPairEvent,
        OrderOrderFunction,
        OrderVariableInstancer>, IEquatable<OrderReference>
    {
        public OrderReference() : base() { }
        public OrderReference(Order value) : base(value) { }
        public bool Equals(OrderReference other) { return base.Equals(other); }
    }
}
