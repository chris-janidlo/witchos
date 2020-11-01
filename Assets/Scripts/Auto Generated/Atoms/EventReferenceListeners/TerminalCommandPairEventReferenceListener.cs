using UnityEngine;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event Reference Listener of type `TerminalCommandPair`. Inherits from `AtomEventReferenceListener&lt;TerminalCommandPair, TerminalCommandPairEvent, TerminalCommandPairEventReference, TerminalCommandPairUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/TerminalCommandPair Event Reference Listener")]
    public sealed class TerminalCommandPairEventReferenceListener : AtomEventReferenceListener<
        TerminalCommandPair,
        TerminalCommandPairEvent,
        TerminalCommandPairEventReference,
        TerminalCommandPairUnityEvent>
    { }
}
