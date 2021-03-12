using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace WitchOS
{
    [Serializable]
    public class FileDoesNotExistException : FilesystemException
    {
        public FileDoesNotExistException () : base() { }
        public FileDoesNotExistException (string message) : base(message) { }
        public FileDoesNotExistException (string message, Exception innerException) : base(message, innerException) { }

        protected FileDoesNotExistException (SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
