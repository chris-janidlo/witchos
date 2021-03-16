using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    [CreateAssetMenu(menuName = "WitchOS/File Association Configuration", fileName = "NewFileAssociation.asset")]
    public class FileAssociationConfig : ScriptableObject
    {
        [Serializable]
        public class FileAssociationData
        {
            public string FullNameOfFileType;
            public WindowMetadata Metadata;
        }

        public List<FileAssociationData> Config;

        public WindowMetadata GetMetadataForFile (FileBase file)
        {
            string fileTypeName = file.GetType().FullName;
            return Config.FirstOrDefault(d => d.FullNameOfFileType == fileTypeName)?.Metadata;
        }
    }
}
