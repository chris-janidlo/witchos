#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using WitchOS;

namespace UnityAtoms.WitchOS.Editor
{
    /// <summary>
    /// Event property drawer of type `RitePair`. Inherits from `AtomEventEditor&lt;RitePair, RitePairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(RitePairEvent))]
    public sealed class RitePairEventEditor : AtomEventEditor<RitePair, RitePairEvent> { }
}
#endif
