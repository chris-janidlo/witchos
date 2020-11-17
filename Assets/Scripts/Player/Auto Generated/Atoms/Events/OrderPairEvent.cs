using UnityEngine;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event of type `OrderPair`. Inherits from `AtomEvent&lt;OrderPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/OrderPair", fileName = "OrderPairEvent")]
    public sealed class OrderPairEvent : AtomEvent<OrderPair> { }
}
