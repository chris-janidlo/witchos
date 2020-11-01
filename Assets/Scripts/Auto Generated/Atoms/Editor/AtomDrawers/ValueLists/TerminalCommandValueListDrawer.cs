#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Value List property drawer of type `TerminalCommand`. Inherits from `AtomDrawer&lt;TerminalCommandValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(TerminalCommandValueList))]
    public class TerminalCommandValueListDrawer : AtomDrawer<TerminalCommandValueList> { }
}
#endif
