using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    [Serializable]
    public class BankSaveData : SaveData<List<BankTransaction>> { }

    [Serializable]
    public class DateTimeSaveData : SaveData<SaveableDate> { }

    [Serializable]
    public class DirectorySaveData : SaveData<Directory> { }

    [Serializable]
    public class IntSaveData : SaveData<int> { }

    [Serializable]
    public class MailSaveData : SaveData<List<MailState.Entry>> { }
}
