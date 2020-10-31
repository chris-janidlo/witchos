using UnityEngine;
using UnityEditor;

namespace WitchOS.Editor
{
    public class PersistentDataPathTool : MonoBehaviour
    {
        [MenuItem("Tools/Copy persistent data path to clipboard")]
        public static void CopyPersistentDataPath ()
        {
            EditorGUIUtility.systemCopyBuffer = Application.persistentDataPath;
        }
    }
}
