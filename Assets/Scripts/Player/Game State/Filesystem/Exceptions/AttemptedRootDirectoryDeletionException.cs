using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace WitchOS
{
    [Serializable]
    public class AttemptedRootDirectoryDeletionException : FilesystemException
    {
        public AttemptedRootDirectoryDeletionException () : base() { }
        public AttemptedRootDirectoryDeletionException (string message) : base(message) { }
        public AttemptedRootDirectoryDeletionException (string message, Exception innerException) : base(message, innerException) { }

        protected AttemptedRootDirectoryDeletionException (SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
