using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace WitchOS
{
    [Serializable]
    public class InvalidPathException : FilesystemException
    {
        public InvalidPathException () : base() { }
        public InvalidPathException (string message) : base(message) { }
        public InvalidPathException (string message, Exception innerException) : base(message, innerException) { }

        protected InvalidPathException (SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
