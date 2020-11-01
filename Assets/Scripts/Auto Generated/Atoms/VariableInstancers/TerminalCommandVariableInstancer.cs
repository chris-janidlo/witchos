using UnityEngine;
using UnityAtoms.BaseAtoms;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Variable Instancer of type `TerminalCommand`. Inherits from `AtomVariableInstancer&lt;TerminalCommandVariable, TerminalCommandPair, TerminalCommand, TerminalCommandEvent, TerminalCommandPairEvent, TerminalCommandTerminalCommandFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/TerminalCommand Variable Instancer")]
    public class TerminalCommandVariableInstancer : AtomVariableInstancer<
        TerminalCommandVariable,
        TerminalCommandPair,
        TerminalCommand,
        TerminalCommandEvent,
        TerminalCommandPairEvent,
        TerminalCommandTerminalCommandFunction>
    { }
}
