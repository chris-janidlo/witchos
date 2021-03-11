using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace WitchOS
{
    /// <summary>
    /// Base exception class for any Filesystem related exceptions. In general, avoid instantiating this, and instantiate one of its subclasses instead.
    /// </summary>
    [Serializable]
    public class FilesystemException : Exception
    {
        public FilesystemException () : base() { }
        public FilesystemException (string message) : base(message) { }
        public FilesystemException (string message, Exception innerException) : base(message, innerException) { }

        protected FilesystemException (SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
