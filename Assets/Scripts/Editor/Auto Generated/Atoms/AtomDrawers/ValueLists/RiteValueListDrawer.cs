#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.WitchOS.Editor
{
    /// <summary>
    /// Value List property drawer of type `Rite`. Inherits from `AtomDrawer&lt;RiteValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(RiteValueList))]
    public class RiteValueListDrawer : AtomDrawer<RiteValueList> { }
}
#endif
