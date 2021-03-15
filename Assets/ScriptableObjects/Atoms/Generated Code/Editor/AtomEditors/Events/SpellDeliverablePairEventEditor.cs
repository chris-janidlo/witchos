#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using WitchOS;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `SpellDeliverablePair`. Inherits from `AtomEventEditor&lt;SpellDeliverablePair, SpellDeliverablePairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(SpellDeliverablePairEvent))]
    public sealed class SpellDeliverablePairEventEditor : AtomEventEditor<SpellDeliverablePair, SpellDeliverablePairEvent> { }
}
#endif
