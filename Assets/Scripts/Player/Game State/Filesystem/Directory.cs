using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace WitchOS
{
    [Serializable, DataContract]
    public class Directory : File<List<FileBase>>, ISerializationCallbackReceiver
    {
        [SerializeField]
        [TextArea(40, 1000)]
        string jsonSerializedData; // Unity doesn't do serialization with polymorphic lists. since file data should always be serializable by DataContractJsonSerializer, this should work with minimal extra effort

        public Directory (string name)
        {
            Name = name;
            Data = new List<FileBase>();
        }

        public Directory (string name, IEnumerable<FileBase> contents) : this(name)
        {
            Data = new List<FileBase>(contents);
        }

        public Directory (string name, params FileBase[] contents)
            : this(name, (IEnumerable<FileBase>) contents)
        { }

        public void OnAfterDeserialize ()
        {
            if (string.IsNullOrEmpty(jsonSerializedData))
            {
                Data = new List<FileBase>();
                return;
            }

            using (var ms = new MemoryStream(Encoding.Default.GetBytes(jsonSerializedData)))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<FileBase>));
                Data = (List<FileBase>) serializer.ReadObject(ms);
            }
        }

        public void OnBeforeSerialize ()
        {
            if (Data == null)
            {
                jsonSerializedData = "";
                return;
            }

            using (var ms = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(List<FileBase>));
                serializer.WriteObject(ms, Data);
                jsonSerializedData = Encoding.Default.GetString(ms.ToArray());
            }
        }
    }
}
