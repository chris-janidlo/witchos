#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `OrderPair`. Inherits from `AtomDrawer&lt;OrderPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(OrderPairEvent))]
    public class OrderPairEventDrawer : AtomDrawer<OrderPairEvent> { }
}
#endif
