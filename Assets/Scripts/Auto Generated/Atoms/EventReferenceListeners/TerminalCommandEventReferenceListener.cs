using UnityEngine;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Event Reference Listener of type `TerminalCommand`. Inherits from `AtomEventReferenceListener&lt;TerminalCommand, TerminalCommandEvent, TerminalCommandEventReference, TerminalCommandUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/TerminalCommand Event Reference Listener")]
    public sealed class TerminalCommandEventReferenceListener : AtomEventReferenceListener<
        TerminalCommand,
        TerminalCommandEvent,
        TerminalCommandEventReference,
        TerminalCommandUnityEvent>
    { }
}
