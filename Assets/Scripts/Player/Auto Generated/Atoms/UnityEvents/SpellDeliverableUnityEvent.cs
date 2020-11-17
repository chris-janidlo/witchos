using System;
using UnityEngine.Events;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// None generic Unity Event of type `SpellDeliverable`. Inherits from `UnityEvent&lt;SpellDeliverable&gt;`.
    /// </summary>
    [Serializable]
    public sealed class SpellDeliverableUnityEvent : UnityEvent<SpellDeliverable> { }
}
