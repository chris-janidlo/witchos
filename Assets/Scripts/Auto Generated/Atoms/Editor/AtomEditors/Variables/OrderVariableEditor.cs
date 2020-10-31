using UnityEditor;
using UnityAtoms.Editor;
using WitchOS;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable Inspector of type `Order`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(OrderVariable))]
    public sealed class OrderVariableEditor : AtomVariableEditor<Order, OrderPair> { }
}
