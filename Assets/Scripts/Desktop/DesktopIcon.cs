using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace WitchOS
{
    public class DesktopIcon : MonoBehaviour
    {
        [Header("Data")]
        public Sprite Icon;
        public string Label;

        [Header("References")]
        public Image IconImage;
        public TextMeshProUGUI LabelText;

        Dictionary<string, Vector3Serializable> posDict;

        void Start ()
        {
            posDict = SaveManager.LooseSaveData.Value.IconPositions;

            if (posDict.ContainsKey(name))
            {
                transform.position = posDict[name];
            }

            SaveManager.LooseSaveData.OnBeforeSave += savePosition;
        }

        void Update ()
        {
            IconImage.sprite = Icon;
            LabelText.text = Label;
        }

        void savePosition ()
        {
            posDict[name] = transform.position;
        }
    }
}
