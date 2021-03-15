#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Constant property drawer of type `Order`. Inherits from `AtomDrawer&lt;OrderConstant&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(OrderConstant))]
    public class OrderConstantDrawer : VariableDrawer<OrderConstant> { }
}
#endif
