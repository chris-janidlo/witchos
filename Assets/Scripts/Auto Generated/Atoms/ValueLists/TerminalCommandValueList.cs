using UnityEngine;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Value List of type `TerminalCommand`. Inherits from `AtomValueList&lt;TerminalCommand, TerminalCommandEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/TerminalCommand", fileName = "TerminalCommandValueList")]
    public sealed class TerminalCommandValueList : AtomValueList<TerminalCommand, TerminalCommandEvent> { }
}
