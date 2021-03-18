using System;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event Reference of type `RitePair`. Inherits from `AtomEventReference&lt;RitePair, RiteVariable, RitePairEvent, RiteVariableInstancer, RitePairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class RitePairEventReference : AtomEventReference<
        RitePair,
        RiteVariable,
        RitePairEvent,
        RiteVariableInstancer,
        RitePairEventInstancer>, IGetEvent 
    { }
}
