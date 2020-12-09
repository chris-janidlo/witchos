using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

namespace WitchOS
{
    // TODO: fully inspector-editable base filesystem
    [Serializable, DataContract]
    public class Directory : File<List<FileBase>>
    {
        public Directory (string name)
        {
            Name = name;
            Data = new List<FileBase>();
        }

        public Directory (string name, IEnumerable<FileBase> contents)
        {
            Name = name;
            Data = new List<FileBase>(contents);
        }

        public Directory (string name, params FileBase[] contents) : this(name, (IEnumerable<FileBase>) contents) { }
    }

    [Serializable, DataContract]
    public class TextFile : File<string> { }

    [Serializable, DataContract]
    public class ExecutableFile : File<WindowMetadata> { }

    // put additional concrete implementations here
}
