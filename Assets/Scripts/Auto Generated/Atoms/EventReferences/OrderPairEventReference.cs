using System;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference of type `OrderPair`. Inherits from `AtomEventReference&lt;OrderPair, OrderVariable, OrderPairEvent, OrderVariableInstancer, OrderPairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class OrderPairEventReference : AtomEventReference<
        OrderPair,
        OrderVariable,
        OrderPairEvent,
        OrderVariableInstancer,
        OrderPairEventInstancer>, IGetEvent 
    { }
}
