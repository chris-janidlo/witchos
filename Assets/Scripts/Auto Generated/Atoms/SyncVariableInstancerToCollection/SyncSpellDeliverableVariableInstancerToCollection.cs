using UnityEngine;
using UnityAtoms.BaseAtoms;
using WitchOS;

namespace UnityAtoms
{
    /// <summary>
    /// Adds Variable Instancer's Variable of type SpellDeliverable to a Collection or List on OnEnable and removes it on OnDestroy. 
    /// </summary>
    [AddComponentMenu("Unity Atoms/Sync Variable Instancer to Collection/Sync SpellDeliverable Variable Instancer to Collection")]
    [EditorIcon("atom-icon-delicate")]
    public class SyncSpellDeliverableVariableInstancerToCollection : SyncVariableInstancerToCollection<SpellDeliverable, SpellDeliverableVariable, SpellDeliverableVariableInstancer> { }
}
