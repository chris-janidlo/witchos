#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.WitchOS.Editor
{
    /// <summary>
    /// Constant property drawer of type `SpellDeliverable`. Inherits from `AtomDrawer&lt;SpellDeliverableConstant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(SpellDeliverableConstant))]
    public class SpellDeliverableConstantDrawer : VariableDrawer<SpellDeliverableConstant> { }
}
#endif
