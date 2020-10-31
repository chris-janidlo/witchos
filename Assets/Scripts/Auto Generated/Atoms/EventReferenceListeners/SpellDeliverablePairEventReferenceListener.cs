using UnityEngine;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference Listener of type `SpellDeliverablePair`. Inherits from `AtomEventReferenceListener&lt;SpellDeliverablePair, SpellDeliverablePairEvent, SpellDeliverablePairEventReference, SpellDeliverablePairUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/SpellDeliverablePair Event Reference Listener")]
    public sealed class SpellDeliverablePairEventReferenceListener : AtomEventReferenceListener<
        SpellDeliverablePair,
        SpellDeliverablePairEvent,
        SpellDeliverablePairEventReference,
        SpellDeliverablePairUnityEvent>
    { }
}
