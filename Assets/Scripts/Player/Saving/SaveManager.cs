using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace WitchOS
{
    [CreateAssetMenu(menuName = "WitchOS/Save Manager", fileName = "SaveManagerSystem.asset")]
    public class SaveManager : ScriptableObject
    {
        HashSet<SaveData> allDataObjects = new HashSet<SaveData>();

        public void Register (SaveData saveData)
        {
            #if UNITY_WEBGL
                return;
            #endif // UNITY_WEBGL

            if (allDataObjects.Contains(saveData)) return;

            if (allDataObjects.Any(s => s.FileName == saveData.FileName))
            {
                throw new InvalidOperationException($"cannot register two save files with the same name ({saveData.FileName})");
            }

            allDataObjects.Add(saveData);
        }

        public void SaveAllData ()
        {
            #if UNITY_WEBGL
                return;
            #endif // UNITY_WEBGL

            foreach (var dataObject in allDataObjects)
            {
                dataObject.WriteDataToFile();
            }
        }

        public void DeleteAllSaveData ()
        {
            #if UNITY_WEBGL
                return;
            #endif // UNITY_WEBGL

            foreach (var dataObject in allDataObjects)
            {
                dataObject.DeleteSaveFile();
            }
        }
    }
}
