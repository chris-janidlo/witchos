using UnityEngine;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event of type `SpellDeliverable`. Inherits from `AtomEvent&lt;SpellDeliverable&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/SpellDeliverable", fileName = "SpellDeliverableEvent")]
    public sealed class SpellDeliverableEvent : AtomEvent<SpellDeliverable> { }
}
