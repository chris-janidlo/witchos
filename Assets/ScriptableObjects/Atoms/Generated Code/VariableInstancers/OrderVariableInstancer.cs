using UnityEngine;
using UnityAtoms.BaseAtoms;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Variable Instancer of type `Order`. Inherits from `AtomVariableInstancer&lt;OrderVariable, OrderPair, Order, OrderEvent, OrderPairEvent, OrderOrderFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/Order Variable Instancer")]
    public class OrderVariableInstancer : AtomVariableInstancer<
        OrderVariable,
        OrderPair,
        Order,
        OrderEvent,
        OrderPairEvent,
        OrderOrderFunction>
    { }
}
