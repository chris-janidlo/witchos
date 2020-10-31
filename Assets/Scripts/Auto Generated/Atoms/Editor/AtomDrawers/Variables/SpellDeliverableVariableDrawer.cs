#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable property drawer of type `SpellDeliverable`. Inherits from `AtomDrawer&lt;SpellDeliverableVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(SpellDeliverableVariable))]
    public class SpellDeliverableVariableDrawer : VariableDrawer<SpellDeliverableVariable> { }
}
#endif
