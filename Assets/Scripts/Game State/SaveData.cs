using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UnityEngine;

public abstract class SaveData
{
    public const string FILE_EXTENSION = ".witchos";

    public Action OnBeforeSave;

    // the part not including the parent directory or the file extension
    public string FileName { get; protected set; }
    public string FilePath => Path.Combine(Application.persistentDataPath, FileName + FILE_EXTENSION);

    public bool DataInitialized { get; protected set; }

    public abstract void InitializeData ();
    public abstract void WriteDataToFile ();
    public abstract void DeleteSaveFile ();
}

public class SaveData<T> : SaveData
    // note that T must be serializable by DataContractSerializer. unfortunately there's no way to check that at compile time, and at runtime you'd have to check if the entire object graph can be serialized, so currently it's up to the developer to ensure they follow this
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

    // default value before any save interactions have ocurred
    T gameDefaultValue;

    DataContractJsonSerializer serializer;

    public SaveData (string fileName, T gameDefaultValue)
    {
        FileName = fileName;
        this.gameDefaultValue = gameDefaultValue;

        serializer = new DataContractJsonSerializer(typeof(T));
    }

    public override void InitializeData ()
    {
        using (FileStream file = File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.Read))
        {
            try
            {
                value = (T) serializer.ReadObject(file);
            }
            catch (Exception ex) when (ex is SerializationException || ex is InvalidCastException)
            {
                Debug.LogWarning(ex);
                Debug.LogWarning($"unable to deserialize save data; using default value {gameDefaultValue}");
                value = gameDefaultValue;
            }
        }

        DataInitialized = true;
    }

    public override void WriteDataToFile ()
    {
        if (!DataInitialized) InitializeData();

        OnBeforeSave?.Invoke();

        using (FileStream file = File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.Write))
        {
            serializer.WriteObject(file, value);
        }
    }

    public override void DeleteSaveFile ()
    {
        File.Delete(FilePath);
    }
}
