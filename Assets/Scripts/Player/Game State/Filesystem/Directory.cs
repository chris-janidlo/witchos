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
    public class Directory : File<List<FileBase>>
    {
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
    }
}
