using UnityEngine;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference Listener of type `OrderPair`. Inherits from `AtomEventReferenceListener&lt;OrderPair, OrderPairEvent, OrderPairEventReference, OrderPairUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/OrderPair Event Reference Listener")]
    public sealed class OrderPairEventReferenceListener : AtomEventReferenceListener<
        OrderPair,
        OrderPairEvent,
        OrderPairEventReference,
        OrderPairUnityEvent>
    { }
}
