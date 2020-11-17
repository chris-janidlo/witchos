#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.WitchOS.Editor
{
    /// <summary>
    /// Value List property drawer of type `SpellDeliverable`. Inherits from `AtomDrawer&lt;SpellDeliverableValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(SpellDeliverableValueList))]
    public class SpellDeliverableValueListDrawer : AtomDrawer<SpellDeliverableValueList> { }
}
#endif
