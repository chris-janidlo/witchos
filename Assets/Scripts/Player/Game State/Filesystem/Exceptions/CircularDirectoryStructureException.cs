using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace WitchOS
{
    [Serializable]
    public class CircularDirectoryStructureException : FilesystemException
    {
        public CircularDirectoryStructureException () : base() { }
        public CircularDirectoryStructureException (string message) : base(message) { }
        public CircularDirectoryStructureException (string message, Exception innerException) : base(message, innerException) { }

        protected CircularDirectoryStructureException (SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
