using UnityEngine;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `TerminalCommandPair`. Inherits from `AtomEvent&lt;TerminalCommandPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/TerminalCommandPair", fileName = "TerminalCommandPairEvent")]
    public sealed class TerminalCommandPairEvent : AtomEvent<TerminalCommandPair> { }
}
