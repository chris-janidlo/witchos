using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WitchOS
{
    public class MirrorCursorSetter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Mirror Mirror;

        bool mousedOver;

        void Update()
        {
            if (Mirror.CurrentState == Mirror.State.Intact && mousedOver && Input.GetMouseButton(0))
            {
                CursorManager.Instance.CursorState = CursorState.HammerSwung;
            }
        }

        public void OnPointerEnter (PointerEventData eventData)
        {
            mousedOver = true;
            if (Mirror.CurrentState == Mirror.State.Intact)
                CursorManager.Instance.CursorState = CursorState.HammerPrimed;
        }

        public void OnPointerExit (PointerEventData eventData)
        {
            mousedOver = false;
            CursorManager.Instance.CursorState = CursorState.Normal;
        }
    }
}
