using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WitchOS
{
    public class MirrorsApp : MonoBehaviour
    {
        public MirrorInfo MirrorInfoPrefab;
        public RectTransform MirrorInfoParent;

        void Start ()
        {
            foreach (var mirror in MirrorState.Instance.Mirrors)
            {
                Instantiate(MirrorInfoPrefab, MirrorInfoParent).SetMirrorState(mirror);
            }
        }
    }
}
