using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

namespace WitchOS
{
    [Serializable, DataContract]
    [KnownType(typeof(Directory))]
    [KnownType(typeof(TextFile))]
    [KnownType(typeof(TextPDFFile))]
    [KnownType(typeof(EmailAppExeFile))]
    [KnownType(typeof(MoonPhaseAppExeFile))]
    [KnownType(typeof(TerminalAppExeFile))]
    [KnownType(typeof(MirrorsAppExeFile))]
    [KnownType(typeof(BankAppExeFile))]
    public abstract class FileBase
    {
        [DataMember(IsRequired = true)]
        public string Name = "";

        [DataMember(IsRequired = true)]
        public SaveableVector3 GuiPosition;

        public abstract Type GetTypeOfData ();
    }

    [Serializable, DataContract]
    [KnownType(typeof(Directory))]
    [KnownType(typeof(TextFile))]
    [KnownType(typeof(TextPDFFile))]
    [KnownType(typeof(EmailAppExeFile))]
    [KnownType(typeof(MoonPhaseAppExeFile))]
    [KnownType(typeof(TerminalAppExeFile))]
    [KnownType(typeof(MirrorsAppExeFile))]
    [KnownType(typeof(BankAppExeFile))]
    public class File<DataType> : FileBase
    // DataType must be DataContract serializable
    {
        //[SerializeReference]
        [DataMember(IsRequired = true)]
        public DataType Data;

        public override Type GetTypeOfData ()
        {
            return typeof(DataType);
        }
    }
}
