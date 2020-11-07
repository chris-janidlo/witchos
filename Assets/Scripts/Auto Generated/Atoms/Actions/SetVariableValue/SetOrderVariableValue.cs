using UnityEngine;
using UnityAtoms.BaseAtoms;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Set variable value Action of type `Order`. Inherits from `SetVariableValue&lt;Order, OrderPair, OrderVariable, OrderConstant, OrderReference, OrderEvent, OrderPairEvent, OrderVariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Order", fileName = "SetOrderVariableValue")]
    public sealed class SetOrderVariableValue : SetVariableValue<
        Order,
        OrderPair,
        OrderVariable,
        OrderConstant,
        OrderReference,
        OrderEvent,
        OrderPairEvent,
        OrderOrderFunction,
        OrderVariableInstancer>
    { }
}
