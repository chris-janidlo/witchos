#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using WitchOS;

namespace UnityAtoms.WitchOS.Editor
{
    /// <summary>
    /// Event property drawer of type `Order`. Inherits from `AtomEventEditor&lt;Order, OrderEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(OrderEvent))]
    public sealed class OrderEventEditor : AtomEventEditor<Order, OrderEvent> { }
}
#endif
