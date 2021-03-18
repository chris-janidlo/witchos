#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using WitchOS;

namespace UnityAtoms.WitchOS.Editor
{
    /// <summary>
    /// Event property drawer of type `Rite`. Inherits from `AtomEventEditor&lt;Rite, RiteEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(RiteEvent))]
    public sealed class RiteEventEditor : AtomEventEditor<Rite, RiteEvent> { }
}
#endif
