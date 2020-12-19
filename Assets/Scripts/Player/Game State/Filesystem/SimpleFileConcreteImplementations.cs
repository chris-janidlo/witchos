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
    public class SaveableWikiPageDataReference : SaveableScriptableObjectReference<WikiPageData> { }

    [Serializable, DataContract]
    public class WikiFile : File<SaveableWikiPageDataReference> { }

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

    [Serializable, DataContract]
    public class SystemAppExeFile : File<SystemAppExeTag> { }
}
