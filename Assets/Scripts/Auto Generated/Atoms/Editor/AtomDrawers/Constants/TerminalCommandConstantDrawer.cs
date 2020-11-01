#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Constant property drawer of type `TerminalCommand`. Inherits from `AtomDrawer&lt;TerminalCommandConstant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(TerminalCommandConstant))]
    public class TerminalCommandConstantDrawer : VariableDrawer<TerminalCommandConstant> { }
}
#endif
