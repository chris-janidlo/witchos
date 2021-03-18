#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.WitchOS.Editor
{
    /// <summary>
    /// Event property drawer of type `RitePair`. Inherits from `AtomDrawer&lt;RitePairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RitePairEvent))]
    public class RitePairEventDrawer : AtomDrawer<RitePairEvent> { }
}
#endif
