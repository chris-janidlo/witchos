using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace WitchOS
{
    [Serializable]
    public class PathDoesNotExistException : FilesystemException
    {
        public PathDoesNotExistException () : base() { }
        public PathDoesNotExistException (string message) : base(message) { }
        public PathDoesNotExistException (string message, Exception innerException) : base(message, innerException) { }

        protected PathDoesNotExistException (SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
