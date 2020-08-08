using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using crass;

namespace WitchOS
{
    public class SOLookupTable : Singleton<SOLookupTable>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<ScriptableObjectPathTuple> serializedTable;

        Dictionary<string, ScriptableObject> forwardDictionary;
        Dictionary<ScriptableObject, string> reverseDictionary;

        void Awake ()
        {
            SingletonOverwriteInstance(this);
        }

        public void OnBeforeSerialize ()
        {
            serializedTable = forwardDictionary.Select(kvp => new ScriptableObjectPathTuple { Path = kvp.Key, Asset = kvp.Value }).ToList();
        }

        public void OnAfterDeserialize ()
        {
            forwardDictionary = serializedTable.ToDictionary(sopt => sopt.Path, sopt => sopt.Asset);
            reverseDictionary = serializedTable.ToDictionary(sopt => sopt.Asset, sopt => sopt.Path);
        }

        public T GetAsset<T> (string path) where T : ScriptableObject
        {
            return forwardDictionary.TryGetValue(path, out ScriptableObject asset) ? asset as T : null;
        }

        public string GetPath (ScriptableObject asset)
        {
            return reverseDictionary.TryGetValue(asset, out string path) ? path : null;
        }

#if UNITY_EDITOR
        public void Write (string path, ScriptableObject asset)
        {
            if (forwardDictionary.ContainsKey(path) || reverseDictionary.ContainsKey(asset))
            {
                Debug.LogWarning($"overwriting lookup table entry for asset at path '{path}'");
            }

            forwardDictionary[path] = asset;
            reverseDictionary[asset] = path;
        }

        public bool Delete (string key)
        {
            var asset = forwardDictionary[key];
            return forwardDictionary.Remove(key) || reverseDictionary.Remove(asset);
        }

        public void Clear ()
        {
            forwardDictionary.Clear();
            reverseDictionary.Clear();
        }
#endif // UNITY_EDITOR
    }
}
