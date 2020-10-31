using UnityEngine;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Event Instancer of type `Order`. Inherits from `AtomEventInstancer&lt;Order, OrderEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/Order Event Instancer")]
    public class OrderEventInstancer : AtomEventInstancer<Order, OrderEvent> { }
}
