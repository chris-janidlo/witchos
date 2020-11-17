using UnityEditor;
using UnityAtoms.Editor;
using WitchOS;

namespace UnityAtoms.WitchOS.Editor
{
    /// <summary>
    /// Variable Inspector of type `TerminalCommand`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(TerminalCommandVariable))]
    public sealed class TerminalCommandVariableEditor : AtomVariableEditor<TerminalCommand, TerminalCommandPair> { }
}
