using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace WitchOS.Tests
{
    [CreateAssetMenu(menuName = "WitchOS/Test Stubs/Save Manager", fileName = "SaveManagerStub.asset")]
    public class SaveManagerStub : SaveManager
    {
        public override void Register (SaveData saveData) { }

        public override void SaveAllData () { }

        public override void DeleteAllSaveData () { }
    }
}
