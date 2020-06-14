#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `SpellDeliverablePair`. Inherits from `AtomDrawer&lt;SpellDeliverablePairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(SpellDeliverablePairEvent))]
    public class SpellDeliverablePairEventDrawer : AtomDrawer<SpellDeliverablePairEvent> { }
}
#endif
