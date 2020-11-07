using UnityEngine;
using UnityEditor;

namespace WitchOS.Editor
{
    public class ForceReserializeTool : MonoBehaviour
    {
        [MenuItem("Tools/Force reserialize all assets")]
        public static void Reserialize ()
        {
            AssetDatabase.ForceReserializeAssets();
        }
    }
}
