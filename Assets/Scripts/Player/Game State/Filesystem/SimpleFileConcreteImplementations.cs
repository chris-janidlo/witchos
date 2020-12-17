using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

namespace WitchOS
{
    [Serializable, DataContract]
    public class TextFile : File<string> { }

    [Serializable, DataContract]
    public class TextPDFFile : File<TextPDF> { }

    [Serializable, DataContract]
    public class EmailAppExeFile : File<EmailAppExeTag> { }

    [Serializable, DataContract]
    public class MoonPhaseAppExeFile : File<MoonPhaseAppExeTag> { }

    [Serializable, DataContract]
    public class TerminalAppExeFile : File<TerminalAppExeTag> { }

    [Serializable, DataContract]
    public class MirrorsAppExeFile : File<MirrorsAppExeTag> { }

    [Serializable, DataContract]
    public class BankAppExeFile : File<BankAppExeTag> { }
}
