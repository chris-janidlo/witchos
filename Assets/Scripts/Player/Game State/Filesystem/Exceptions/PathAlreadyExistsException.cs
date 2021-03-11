using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace WitchOS
{
    [Serializable]
    public class PathAlreadyExistsException : FilesystemException
    {
        public PathAlreadyExistsException () : base() { }
        public PathAlreadyExistsException (string message) : base(message) { }
        public PathAlreadyExistsException (string message, Exception innerException) : base(message, innerException) { }

        protected PathAlreadyExistsException (SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
