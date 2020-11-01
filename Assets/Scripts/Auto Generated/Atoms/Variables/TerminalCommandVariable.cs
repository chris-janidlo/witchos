using UnityEngine;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Variable of type `TerminalCommand`. Inherits from `EquatableAtomVariable&lt;TerminalCommand, TerminalCommandPair, TerminalCommandEvent, TerminalCommandPairEvent, TerminalCommandTerminalCommandFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/TerminalCommand", fileName = "TerminalCommandVariable")]
    public sealed class TerminalCommandVariable : EquatableAtomVariable<TerminalCommand, TerminalCommandPair, TerminalCommandEvent, TerminalCommandPairEvent, TerminalCommandTerminalCommandFunction> { }
}
