using System;
using UnityEngine;
using UnityEditor;
using crass;

public class TermComAutoAssetCreator : MonoBehaviour
{
    public const string ASSET_PATH = "Assets/ScriptableObjects/TerminalCommands";

    [MenuItem("Tools/Generate TerminalCommand ScriptableObjects")] // for manual use
    [UnityEditor.Callbacks.DidReloadScripts] // automatic trigger every compilation
    public static void GenerateAllTerminalCommands ()
    {
        bool needToSave = false;

        var assetFolders = new[] { ASSET_PATH };

        foreach (Type type in Reflection.GetImplementations<TerminalCommand>())
        {
            if (AssetDatabase.FindAssets(type.Name, assetFolders).Length != 0) continue;

            var command = ScriptableObject.CreateInstance(type);
            AssetDatabase.CreateAsset(command, ASSET_PATH + "/" + type.Name + ".asset");

            needToSave = true;
        }

        // note: due to a known issue, the SaveAssets call throws an error in play mode
        // see https://issuetracker.unity3d.com/issues/assetdatabase-dot-saveassets-throws-an-exception-the-specified-path-is-not-of-a-legal-form-empty-while-in-play-mode
        if (needToSave) AssetDatabase.SaveAssets();
    }
}
