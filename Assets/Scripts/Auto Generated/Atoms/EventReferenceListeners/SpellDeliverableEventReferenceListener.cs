using UnityEngine;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event Reference Listener of type `SpellDeliverable`. Inherits from `AtomEventReferenceListener&lt;SpellDeliverable, SpellDeliverableEvent, SpellDeliverableEventReference, SpellDeliverableUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/SpellDeliverable Event Reference Listener")]
    public sealed class SpellDeliverableEventReferenceListener : AtomEventReferenceListener<
        SpellDeliverable,
        SpellDeliverableEvent,
        SpellDeliverableEventReference,
        SpellDeliverableUnityEvent>
    { }
}
