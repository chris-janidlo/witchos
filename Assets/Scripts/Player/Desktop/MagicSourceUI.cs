using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WitchOS
{
    public class MagicSourceUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public Animator Animator;
        public string HighlightBool, OnBool, OffAnimation;
        
        public TimeState TimeState;

        bool mouseOver;

        void Awake ()
        {
            TimeState.DayEnded.AddListener(() => Animator.Play(OffAnimation));
        }

        void Update ()
        {
            Animator.SetBool(HighlightBool, MagicSource.Instance.Off && mouseOver);
            Animator.SetBool(OnBool, MagicSource.Instance.On);
        }

        public void OnPointerEnter (PointerEventData eventData)
        {
            mouseOver = true;
        }

        public void OnPointerExit (PointerEventData eventData)
        {
            mouseOver = false;
        }

        public void OnPointerClick (PointerEventData eventData)
        {
            if (!MagicSource.Instance.Off) return;
            MagicSource.Instance.TurnOn();
        }
    }
}
