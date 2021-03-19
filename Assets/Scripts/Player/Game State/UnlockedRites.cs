using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    [CreateAssetMenu(menuName = "WitchOS/Unlocked Rite List", fileName = "newUnlockedRiteList.asset")]
    public class UnlockedRites : InitializableSO
    {
        public IEnumerable<Rite> List => SaveData.Value.Select(srr => srr.Value);

        public UnlockedRiteSaveData SaveData;
        public SaveManager SaveManager;

        public override void Initialize ()
        {
            SaveManager.Register(SaveData);
        }

        public void Add (params Rite[] rites)
        {
            SaveData.Value.AddRange(rites.Select(r => new SaveableRiteReference { Value = r }));
        }
    }
}
