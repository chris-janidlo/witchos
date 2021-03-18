#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.WitchOS.Editor
{
    /// <summary>
    /// Constant property drawer of type `Rite`. Inherits from `AtomDrawer&lt;RiteConstant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RiteConstant))]
    public class RiteConstantDrawer : VariableDrawer<RiteConstant> { }
}
#endif
