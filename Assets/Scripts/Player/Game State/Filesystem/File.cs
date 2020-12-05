using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

namespace WitchOS
{
    [Serializable, DataContract]
    public abstract class FileBase
    {
        [DataMember(IsRequired = true)]
        public string Name = "";

        [DataMember(IsRequired = true)]
        public SaveableVector3 GuiPosition;
    }

    [Serializable, DataContract]
    [KnownType(typeof(Directory))]
    [KnownType(typeof(TextFile))]
    public class File<DataType> : FileBase
    // DataType must be DataContract serializable
    {
        [SerializeReference]
        [DataMember(IsRequired = true)]
        public DataType Data;
    }
}
