using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace WitchOS
{
    public class SOLookupTableAutoPopulator : AssetPostprocessor
    {
        const string SO_LOOKUP_TABLE_PATH = "Assets/Prefabs/SO Lookup Table.prefab";

        // every ScriptableObject in these directories is automatically added to the lookup table prefab
        static readonly string[] DIRECTORIES_TO_TRACK = new string[]
        {
            "Assets/ScriptableObjects/Emails",
            "Assets/ScriptableObjects/Invoices"
        };

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "this is an AssetPostProcessor message")]
        static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            var lutPrefab = AssetDatabase.LoadAssetAtPath(SO_LOOKUP_TABLE_PATH, typeof(GameObject)) as GameObject;
            var soLookupTable = lutPrefab.GetComponent<SOLookupTable>();

            // remove first in case asset moved between two tracked directories
            foreach (var path in pathsInTrackedDirectories(deletedAssets.Concat(movedFromAssetPaths)))
            {
                soLookupTable.LookUpTable.RemoveAll(o => o.Path == path);
            }

            var toAdd = pathsInTrackedDirectories(importedAssets.Concat(movedAssets))
                .Where(p => AssetDatabase.GetMainAssetTypeAtPath(p).IsSubclassOf(typeof(ScriptableObject)));

            foreach (var path in toAdd)
            {
                if (!soLookupTable.LookUpTable.Any(o => o.Path == path))
                    soLookupTable.LookUpTable.Add(new ScriptableObjectPathTuple(path));
            }

            //PrefabUtility.SavePrefabAsset(lutPrefab);
        }

        static IEnumerable<string> pathsInTrackedDirectories (IEnumerable<string> pathList)
        {
            return pathList.Where(p => DIRECTORIES_TO_TRACK.Any(d => p.StartsWith(d)));
        }
    }
}
