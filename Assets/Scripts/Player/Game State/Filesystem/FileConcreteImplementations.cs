using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

namespace WitchOS
{
    // TODO: fully inspector-editable base filesystem
    [DataContract]
    public class Directory : File<List<FileBase>>
    {
        public Directory (string name)
        {
            Name = name;
            Data = new List<FileBase>();
        }
    }

    // put additional concrete implementations here
}
