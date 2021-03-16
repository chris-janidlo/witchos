#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using WitchOS;

namespace UnityAtoms.WitchOS.Editor
{
    /// <summary>
    /// Event property drawer of type `TerminalCommandPair`. Inherits from `AtomEventEditor&lt;TerminalCommandPair, TerminalCommandPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(TerminalCommandPairEvent))]
    public sealed class TerminalCommandPairEventEditor : AtomEventEditor<TerminalCommandPair, TerminalCommandPairEvent> { }
}
#endif
