using UnityEngine;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event Reference Listener of type `RitePair`. Inherits from `AtomEventReferenceListener&lt;RitePair, RitePairEvent, RitePairEventReference, RitePairUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/RitePair Event Reference Listener")]
    public sealed class RitePairEventReferenceListener : AtomEventReferenceListener<
        RitePair,
        RitePairEvent,
        RitePairEventReference,
        RitePairUnityEvent>
    { }
}
