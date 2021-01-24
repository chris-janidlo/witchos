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
    [KnownType(typeof(WikiFile))]
    [KnownType(typeof(SystemAppExeFile))]
    public abstract class FileBase
    {
        [DataMember(IsRequired = true)]
        public string Name = "";

        [DataMember(IsRequired = true)]
        public SaveableVector3 GuiPosition;

        public abstract T GetData<T> () where T : class;
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
    [KnownType(typeof(WikiFile))]
    [KnownType(typeof(SystemAppExeFile))]
    public class File<DataType> : FileBase
    // DataType must be DataContract serializable
    {
        [DataMember(IsRequired = true)]
        public DataType Data;

        public override T GetData<T> ()
        {
            Type t = typeof(T), dataType = typeof(DataType);

            if (t != dataType)
            {
                throw new ArgumentException($"cannot get data of type {t.FullName} from a file with data of type {dataType.FullName}");
            }

            return Data as T;
        }
    }
}
