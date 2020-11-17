#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.WitchOS.Editor
{
    /// <summary>
    /// Value List property drawer of type `Order`. Inherits from `AtomDrawer&lt;OrderValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(OrderValueList))]
    public class OrderValueListDrawer : AtomDrawer<OrderValueList> { }
}
#endif
