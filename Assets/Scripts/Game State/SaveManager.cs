using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace WitchOS
{
public static class SaveManager
{
    public static SaveData<LooseSaveValues> LooseSaveData { get; private set; }

    static HashSet<SaveData> allDataObjects;

    static SaveManager ()
    {
        LooseSaveData = new SaveData<LooseSaveValues>
        (
            "looseData",
            new LooseSaveValues
            {
                Date = TimeState.INITIAL_DATE,
                CurrentDifficultyLevel = 0,
                IconPositions = new Dictionary<string, Vector3Serializable>()
            }
        );

        allDataObjects = new HashSet<SaveData> { LooseSaveData };
    }

    public static void RegisterSaveDataObject (SaveData saveData)
    {
        allDataObjects.Add(saveData);
    }

    public static void SaveAllData ()
    {
        foreach (var dataObject in allDataObjects)
        {
            dataObject.WriteDataToFile();
        }
    }

    public static void DeleteAllSaveData ()
    {
        foreach (var dataObject in allDataObjects)
        {
            dataObject.DeleteSaveFile();
        }
    }
}

[DataContract]
public class LooseSaveValues
{
    [DataMember(IsRequired = true)]
    public DateTime Date;
    [DataMember(IsRequired = true)]
    public int CurrentDifficultyLevel;
    [DataMember(IsRequired = true)]
    public Dictionary<string, Vector3Serializable> IconPositions;
}
}
