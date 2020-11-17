#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.WitchOS.Editor
{
    /// <summary>
    /// Variable property drawer of type `TerminalCommand`. Inherits from `AtomDrawer&lt;TerminalCommandVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(TerminalCommandVariable))]
    public class TerminalCommandVariableDrawer : VariableDrawer<TerminalCommandVariable> { }
}
#endif
