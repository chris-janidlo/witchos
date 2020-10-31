#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using WitchOS;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `OrderPair`. Inherits from `AtomEventEditor&lt;OrderPair, OrderPairEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(OrderPairEvent))]
    public sealed class OrderPairEventEditor : AtomEventEditor<OrderPair, OrderPairEvent> { }
}
#endif
