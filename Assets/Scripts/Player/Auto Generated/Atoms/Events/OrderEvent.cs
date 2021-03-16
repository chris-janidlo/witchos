using UnityEngine;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event of type `Order`. Inherits from `AtomEvent&lt;Order&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Order", fileName = "OrderEvent")]
    public sealed class OrderEvent : AtomEvent<Order> { }
}
