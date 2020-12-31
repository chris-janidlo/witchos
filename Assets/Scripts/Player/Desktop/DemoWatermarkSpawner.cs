using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using crass;

namespace WitchOS
{
    public class DemoWatermarkSpawner : MonoBehaviour
    {
        public DemoWatermark WatermarkPrefab;

#if UNITY_WEBGL
        void Start ()
        {
            Instantiate(WatermarkPrefab, transform);
        }
#endif // UNITY_WEBGL
    }
}
