#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `TerminalCommandPair`. Inherits from `AtomDrawer&lt;TerminalCommandPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(TerminalCommandPairEvent))]
    public class TerminalCommandPairEventDrawer : AtomDrawer<TerminalCommandPairEvent> { }
}
#endif
