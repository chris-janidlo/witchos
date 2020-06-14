using UnityEngine;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Value List of type `Order`. Inherits from `AtomValueList&lt;Order, OrderEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/Order", fileName = "OrderValueList")]
    public sealed class OrderValueList : AtomValueList<Order, OrderEvent> { }
}
