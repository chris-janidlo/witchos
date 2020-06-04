using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    public static SaveData<LooseSaveValues> LooseSaveData { get; private set; }

    static HashSet<SaveData> allDataObjects;

    static SaveManager ()
    {
        LooseSaveData = new SaveData<LooseSaveValues>
        (
            "looseData",
            new LooseSaveValues { Date = new DateTime(2000, 10, 13) }
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

public class LooseSaveValues
{
    public DateTime Date;
    // list of icon positions
}
