#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `TerminalCommand`. Inherits from `AtomDrawer&lt;TerminalCommandEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(TerminalCommandEvent))]
    public class TerminalCommandEventDrawer : AtomDrawer<TerminalCommandEvent> { }
}
#endif
