using UnityEngine;
using UnityAtoms.BaseAtoms;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Adds Variable Instancer's Variable of type Rite to a Collection or List on OnEnable and removes it on OnDestroy. 
    /// </summary>
    [AddComponentMenu("Unity Atoms/Sync Variable Instancer to Collection/Sync Rite Variable Instancer to Collection")]
    [EditorIcon("atom-icon-delicate")]
    public class SyncRiteVariableInstancerToCollection : SyncVariableInstancerToCollection<Rite, RiteVariable, RiteVariableInstancer> { }
}
