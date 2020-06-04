using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
            if (this.value.Equals(value)) return;
            this.value = value;
        }
    }

    // the part not including the parent directory or the file extension
    public string FileName { get; private set; }
    public string FilePath => Path.Combine(Application.persistentDataPath, FileName + FILE_EXTENSION);

    public bool DataInitialized { get; private set; }

    // default value before any save interactions have ocurred
    T gameDefaultValue;

    BinaryFormatter binaryFormatter = new BinaryFormatter();

    public SaveData (string fileName, T gameDefaultValue)
    {
        FileName = fileName;
        this.gameDefaultValue = gameDefaultValue;
    }

    public override void InitializeData ()
    {
        using (FileStream file = File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.Read))
        {
            try
            {
                value = (T) binaryFormatter.Deserialize(file);
            }
            catch (Exception ex) when (ex is SerializationException || ex is InvalidCastException)
            {
                Debug.LogWarning($"unable to deserialize save data; using default value {gameDefaultValue}");
                value = gameDefaultValue;
            }
        }

        DataInitialized = true;
    }

    public override void WriteDataToFile ()
    {
        if (!DataInitialized) InitializeData();

        using (FileStream file = File.Open(FilePath, FileMode.Open, FileAccess.Write))
        {
            binaryFormatter.Serialize(file, value);
        }
    }

    public override void DeleteSaveFile ()
    {
        File.Delete(FilePath);
    }
}
