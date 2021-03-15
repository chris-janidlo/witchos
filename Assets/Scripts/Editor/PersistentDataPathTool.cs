using UnityEngine;
using UnityEditor;

public class PersistentDataPathTool : MonoBehaviour
{
    [MenuItem("Tools/Copy persistent data path to clipboard")]
    public static void CopyPersistentDataPath ()
    {
        EditorGUIUtility.systemCopyBuffer = Application.persistentDataPath;
    }
}
