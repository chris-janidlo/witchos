using UnityEngine;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event Reference Listener of type `Rite`. Inherits from `AtomEventReferenceListener&lt;Rite, RiteEvent, RiteEventReference, RiteUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Rite Event Reference Listener")]
    public sealed class RiteEventReferenceListener : AtomEventReferenceListener<
        Rite,
        RiteEvent,
        RiteEventReference,
        RiteUnityEvent>
    { }
}
