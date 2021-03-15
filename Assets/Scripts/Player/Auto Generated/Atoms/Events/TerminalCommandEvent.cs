using UnityEngine;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event of type `TerminalCommand`. Inherits from `AtomEvent&lt;TerminalCommand&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/TerminalCommand", fileName = "TerminalCommandEvent")]
    public sealed class TerminalCommandEvent : AtomEvent<TerminalCommand> { }
}
