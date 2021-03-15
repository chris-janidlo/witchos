using UnityEngine;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference Listener of type `Order`. Inherits from `AtomEventReferenceListener&lt;Order, OrderEvent, OrderEventReference, OrderUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Order Event Reference Listener")]
    public sealed class OrderEventReferenceListener : AtomEventReferenceListener<
        Order,
        OrderEvent,
        OrderEventReference,
        OrderUnityEvent>
    { }
}
