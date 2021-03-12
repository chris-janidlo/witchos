using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace WitchOS
{
    [Serializable]
    public class FileAlreadyExistsException : FilesystemException
    {
        public FileAlreadyExistsException () : base() { }
        public FileAlreadyExistsException (string message) : base(message) { }
        public FileAlreadyExistsException (string message, Exception innerException) : base(message, innerException) { }

        protected FileAlreadyExistsException (SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
