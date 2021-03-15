using UnityEditor;
using UnityAtoms.Editor;
using WitchOS;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable Inspector of type `SpellDeliverable`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(SpellDeliverableVariable))]
    public sealed class SpellDeliverableVariableEditor : AtomVariableEditor<SpellDeliverable, SpellDeliverablePair> { }
}
