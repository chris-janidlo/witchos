using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

namespace WitchOS
{
    [DataContract]
    public class Directory : File<List<FileBase>> { }

    // put additional concrete implementations here
}
