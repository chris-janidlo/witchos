using UnityEngine;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event Instancer of type `OrderPair`. Inherits from `AtomEventInstancer&lt;OrderPair, OrderPairEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/OrderPair Event Instancer")]
    public class OrderPairEventInstancer : AtomEventInstancer<OrderPair, OrderPairEvent> { }
}
