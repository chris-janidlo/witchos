using UnityEngine;
using UnityAtoms.BaseAtoms;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Adds Variable Instancer's Variable of type Order to a Collection or List on OnEnable and removes it on OnDestroy. 
    /// </summary>
    [AddComponentMenu("Unity Atoms/Sync Variable Instancer to Collection/Sync Order Variable Instancer to Collection")]
    [EditorIcon("atom-icon-delicate")]
    public class SyncOrderVariableInstancerToCollection : SyncVariableInstancerToCollection<Order, OrderVariable, OrderVariableInstancer> { }
}
