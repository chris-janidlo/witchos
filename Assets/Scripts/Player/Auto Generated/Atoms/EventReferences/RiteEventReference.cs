using System;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event Reference of type `Rite`. Inherits from `AtomEventReference&lt;Rite, RiteVariable, RiteEvent, RiteVariableInstancer, RiteEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class RiteEventReference : AtomEventReference<
        Rite,
        RiteVariable,
        RiteEvent,
        RiteVariableInstancer,
        RiteEventInstancer>, IGetEvent 
    { }
}
