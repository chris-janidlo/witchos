using UnityEngine;
using WitchOS;

namespace UnityAtoms.WitchOS
{
    /// <summary>
    /// Event of type `RitePair`. Inherits from `AtomEvent&lt;RitePair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/RitePair", fileName = "RitePairEvent")]
    public sealed class RitePairEvent : AtomEvent<RitePair> { }
}
