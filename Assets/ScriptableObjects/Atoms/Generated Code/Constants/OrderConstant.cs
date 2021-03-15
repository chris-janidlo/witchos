using UnityEngine;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Constant of type `Order`. Inherits from `AtomBaseVariable&lt;Order&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-teal")]
    [CreateAssetMenu(menuName = "Unity Atoms/Constants/Order", fileName = "OrderConstant")]
    public sealed class OrderConstant : AtomBaseVariable<Order> { }
}
