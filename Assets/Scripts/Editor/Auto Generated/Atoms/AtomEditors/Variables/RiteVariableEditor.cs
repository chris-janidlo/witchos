using UnityEditor;
using UnityAtoms.Editor;
using WitchOS;

namespace UnityAtoms.WitchOS.Editor
{
    /// <summary>
    /// Variable Inspector of type `Rite`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(RiteVariable))]
    public sealed class RiteVariableEditor : AtomVariableEditor<Rite, RitePair> { }
}
