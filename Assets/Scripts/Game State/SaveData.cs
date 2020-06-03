using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveData
{
    public const string FILE_EXTENSION = ".witchos";

    public abstract void InitializeData ();
    public abstract void WriteDataToFile ();
    public abstract void DeleteSaveFile ();
}

public class SaveData<T> : SaveData
    // where T : BinarySerializable
{
    T value;
    public T Value
    {
        get
        {
            if (!DataInitialized) InitializeData();
            return value;
        }

        set
        {
            this.value = value;
            DataChangedSinceLastWrite = true;
        }
    }

    // the part not including the parent directory or the file extension
    public string FileName { get; private set; }
    public string FilePath => Path.Combine(Application.persistentDataPath, FileName + FILE_EXTENSION);
    
    public bool DataChangedSinceLastWrite { get; private set; }
    public bool DataInitialized { get; private set; }

    // default value before any save interactions have ocurred
    T gameDefaultValue;

    public SaveData (string fileName, T gameDefaultValue)
    {
        FileName = fileName;
        this.gameDefaultValue = gameDefaultValue;
    }

    public override void InitializeData ()
    {
        // if file doesn't exist: set value to gameDefaultValue
        // else: load the data from FilePath

        DataInitialized = true;
    }

    public override void WriteDataToFile ()
    {
        if (!DataChangedSinceLastWrite) return;

        // save the data to FilePath
    }

    public override void DeleteSaveFile ()
    {
        throw new NotImplementedException();
    }
}
