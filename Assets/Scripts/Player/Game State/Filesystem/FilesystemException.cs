using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{

    public class FilesystemException : Exception
    {
        public FilesystemException () : base() { }
        public FilesystemException (string message) : base(message) { }
        public FilesystemException (string message, Exception innerException) : base (message, innerException) { }
    }
}
