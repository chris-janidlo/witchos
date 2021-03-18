#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.WitchOS.Editor
{
    /// <summary>
    /// Variable property drawer of type `Rite`. Inherits from `AtomDrawer&lt;RiteVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RiteVariable))]
    public class RiteVariableDrawer : VariableDrawer<RiteVariable> { }
}
#endif
