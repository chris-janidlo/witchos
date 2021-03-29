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

    // need this in order to be able to edit wiki files in inspector
    [Serializable, DataContract]
    public class SaveableWikiPageDataReference : SaveableScriptableObjectReference<WikiPageData> { }

    [Serializable, DataContract]
    public class EmotionFile : File<Emotion> { }

    [Serializable, DataContract]
    public class WikiFile : File<SaveableWikiPageDataReference> { }

    [Serializable, DataContract]
    public class EmailFile : File<Email> { }

    [Serializable, DataContract]
    public class OrderFile : File<Order> { }

    [Serializable, DataContract]
    public class BankAppExeFile : File<ExeFileData> { }

    [Serializable, DataContract]
    public class EmailAppExeFile : File<ExeFileData> { }

    [Serializable, DataContract]
    public class MirrorsAppExeFile : File<ExeFileData> { }

    [Serializable, DataContract]
    public class MoonPhaseAppExeFile : File<ExeFileData> { }

    [Serializable, DataContract]
    public class TerminalAppExeFile : File<ExeFileData> { }

    [Serializable, DataContract]
    public class SystemAppExeFile : File<ExeFileData> { }

    [Serializable, DataContract]
    public class RitualStudioAppExeFile : File<ExeFileData> { }
}
