using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    // editor-friendly container for generic file data
    public abstract class FileSOBase : ScriptableObject
    {
        public abstract FileBase File { get; }
    }

    public class FileSO<T> : FileSOBase where T : FileBase
    {
        [SerializeField]
        private T _file = null;

        public override FileBase File => _file;
    }
}
