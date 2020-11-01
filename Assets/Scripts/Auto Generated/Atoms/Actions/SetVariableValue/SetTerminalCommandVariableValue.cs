using UnityEngine;
using UnityAtoms.BaseAtoms;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Set variable value Action of type `TerminalCommand`. Inherits from `SetVariableValue&lt;TerminalCommand, TerminalCommandPair, TerminalCommandVariable, TerminalCommandConstant, TerminalCommandReference, TerminalCommandEvent, TerminalCommandPairEvent, TerminalCommandVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/TerminalCommand", fileName = "SetTerminalCommandVariableValue")]
    public sealed class SetTerminalCommandVariableValue : SetVariableValue<
        TerminalCommand,
        TerminalCommandPair,
        TerminalCommandVariable,
        TerminalCommandConstant,
        TerminalCommandReference,
        TerminalCommandEvent,
        TerminalCommandPairEvent,
        TerminalCommandTerminalCommandFunction,
        TerminalCommandVariableInstancer>
    { }
}
