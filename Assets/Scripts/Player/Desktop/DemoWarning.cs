using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class DemoWarning : MonoBehaviour
    {
        [TextArea]
        public string WarningText;

        public float Delay;

        bool warned;

        public void OnNewDay ()
        {
#if UNITY_WEBGL
            if (warned) return;
            warned = true;
            StartCoroutine(warnRoutine());
        }

        IEnumerator warnRoutine ()
        { 
            yield return new WaitForSeconds(Delay);
            Alert.Instance.ShowMessageImmediately(WarningText);
#endif // UNITY_WEBGL
        }
    }
}
