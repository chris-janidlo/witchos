using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class RiteAlerter : MonoBehaviour
    {
        public void OnRiteCompleted (Rite rite)
        {
            Alert.Instance.ShowMessage($"Performed the {rite.Name} rite.");
        }
    }
}
