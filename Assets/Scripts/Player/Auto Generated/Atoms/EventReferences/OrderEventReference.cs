using System;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event Reference of type `Order`. Inherits from `AtomEventReference&lt;Order, OrderVariable, OrderEvent, OrderVariableInstancer, OrderEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class OrderEventReference : AtomEventReference<
        Order,
        OrderVariable,
        OrderEvent,
        OrderVariableInstancer,
        OrderEventInstancer>, IGetEvent 
    { }
}
