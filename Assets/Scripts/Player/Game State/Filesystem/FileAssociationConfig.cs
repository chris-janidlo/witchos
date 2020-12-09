using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class FileAssociationConfig : ScriptableObject
    {
        // TODO: might need some kind of generic fallback. alternatively, could have one association with type "object"

        public class FileAssociationData
        {
            public Type DataType;
            public WindowMetadata IconMetadata;
        }

        public List<FileAssociationData> Config;

        public WindowMetadata GetFileAssociations (Type fileDataType)
        {
            return Config.FirstOrDefault(d => d.DataType == fileDataType).IconMetadata;
        }

        public WindowMetadata GetFileAssociations<T> ()
        {
            return GetFileAssociations(typeof(T));
        }
    }
}
