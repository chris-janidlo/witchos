#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable property drawer of type `Order`. Inherits from `AtomDrawer&lt;OrderVariable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(OrderVariable))]
    public class OrderVariableDrawer : VariableDrawer<OrderVariable> { }
}
#endif
