using System;
using UnityEngine;
using UnityEditor;

namespace WitchOS
{
    [Serializable]
    public class ScriptableObjectPathTuple
    {
        public ScriptableObject Asset;
        public string Path;

        public ScriptableObjectPathTuple (string path)
        {
            Path = path;
            Asset = AssetDatabase.LoadAssetAtPath(path, typeof(ScriptableObject)) as ScriptableObject;
        }
    }
}
