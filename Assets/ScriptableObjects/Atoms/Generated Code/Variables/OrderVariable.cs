using UnityEngine;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Variable of type `Order`. Inherits from `EquatableAtomVariable&lt;Order, OrderPair, OrderEvent, OrderPairEvent, OrderOrderFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Order", fileName = "OrderVariable")]
    public sealed class OrderVariable : EquatableAtomVariable<Order, OrderPair, OrderEvent, OrderPairEvent, OrderOrderFunction> { }
}
