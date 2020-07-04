#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `SpellDeliverable`. Inherits from `AtomDrawer&lt;SpellDeliverableEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(SpellDeliverableEvent))]
    public class SpellDeliverableEventDrawer : AtomDrawer<SpellDeliverableEvent> { }
}
#endif
