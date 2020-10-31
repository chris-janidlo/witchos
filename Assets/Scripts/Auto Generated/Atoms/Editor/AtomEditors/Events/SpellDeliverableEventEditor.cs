#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using WitchOS;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `SpellDeliverable`. Inherits from `AtomEventEditor&lt;SpellDeliverable, SpellDeliverableEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(SpellDeliverableEvent))]
    public sealed class SpellDeliverableEventEditor : AtomEventEditor<SpellDeliverable, SpellDeliverableEvent> { }
}
#endif
