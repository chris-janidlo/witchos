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

#if UNITY_WEBGL
        IEnumerator Start ()
        {
            yield return new WaitForSeconds(Delay);
            Alert.Instance.ShowMessageImmediately(WarningText);
        }
#endif // UNITY_WEBGL
    }
}
