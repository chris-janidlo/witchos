using System;
using UnityEngine.Events;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// None generic Unity Event of type `Order`. Inherits from `UnityEvent&lt;Order&gt;`.
    /// </summary>
    [Serializable]
    public sealed class OrderUnityEvent : UnityEvent<Order> { }
}
