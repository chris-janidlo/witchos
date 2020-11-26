using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

namespace WitchOS
{
    [DataContract]
    public abstract class FileBase
    {
        [DataMember(IsRequired = true)]
        public string Name;

        [DataMember(IsRequired = true)]
        public SaveableVector3 GuiPosition;
    }

    [DataContract]
    [KnownType(typeof(Directory))]
    public class File<DataType> : FileBase
    // DataType must be DataContract serializable
    {
        [DataMember(IsRequired = true)]
        public DataType Data;
    }
}
