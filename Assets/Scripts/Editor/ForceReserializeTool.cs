using UnityEngine;
using UnityEditor;

public class ForceReserializeTool : MonoBehaviour
{
    [MenuItem("Tools/Force reserialize all assets")]
    public static void Reserialize ()
    {
        AssetDatabase.ForceReserializeAssets();
    }
}
