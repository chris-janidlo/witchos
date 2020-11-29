using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using UnityEngine;

namespace WitchOS
{
    public abstract class SaveData
    {
        public const string FILE_EXTENSION = ".witchos";

        public Action OnBeforeSave;

        [SerializeField]
        [TextArea]
        private string DeveloperNotes;

        [Tooltip("The part of the name of the save file that does not include the directory or the file extension. So if you want to save the data to '/application/persistent-data-path/saveFile.extension', set this to 'saveFile'.")]
        public string FileName;

        public string FilePath => Path.Combine(Application.persistentDataPath, FileName + FILE_EXTENSION);

        public abstract void WriteDataToFile ();
        public abstract void DeleteSaveFile ();
    }

    [Serializable]
    public class SaveData<T> : SaveData
    // note that T must be serializable by DataContractSerializer. unfortunately there's no way to check that at compile time, and at runtime you'd have to check if the entire object graph can be serialized, so currently it's up to the developer to ensure they follow this
    {
        [SerializeField]
        [Tooltip("The current value of the data. Whatever this is set to in the editor is used as the default value for a fresh save.")]
        T value;
        public T Value
        {
            get
            {
                if (!dataInitialized) initializeData();
                return value;
            }

            set => this.value = value;
        }

        DataContractJsonSerializer serializer;
        bool dataInitialized;

        public override void WriteDataToFile ()
        {
            if (!dataInitialized) initializeData();

            OnBeforeSave?.Invoke();

            using (FileStream file = File.Open(FilePath, FileMode.Create, FileAccess.Write))
            {
                serializer.WriteObject(file, value);
            }
        }

        public override void DeleteSaveFile ()
        {
            File.Delete(FilePath);
        }

        void initializeData ()
        {
            serializer = new DataContractJsonSerializer(typeof(T));

            using (FileStream file = File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                try
                {
                    Value = (T) serializer.ReadObject(file);
                }
                catch (Exception ex) when (ex is SerializationException)
                {
                    // always do nothing; the default value is already supplied
#if UNITY_EDITOR
                    // additionally, when in editor, write logs to verify that the error handling is correct
                    Debug.Log(ex);
                    Debug.Log("unable to deserialize save data; using default value.");
#endif // UNITY_EDITOR
                }
            }

            dataInitialized = true;
        }
    }
}
