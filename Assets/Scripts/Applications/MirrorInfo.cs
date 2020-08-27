using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WitchOS
{
    public class MirrorInfo : MonoBehaviour
    {
        public Animator MirrorAnimator;
        public string AnimatorStateIntegerName, AnimatorRepairProgressFloatName;

        public Image Clock, ClockOverlay;
        public Sprite RegularClockSprite, BrokenClockSprite;
        public int ClockFillSegments;

        public Button BreakButton;

        MirrorState.Mirror mirror;

        void Update ()
        {
            if (mirror == null) return;

            MirrorAnimator.SetInteger(AnimatorStateIntegerName, (int) mirror.State);

            BreakButton.interactable = mirror.State == MirrorState.State.Intact;

            Clock.sprite = mirror.State == MirrorState.State.Dud
                ? BrokenClockSprite
                : RegularClockSprite;

            float fillAmount;

            switch (mirror.State)
            {
                case MirrorState.State.Broken:
                    fillAmount = mirror.Timer / mirror.TimeUntilDud;
                    break;

                case MirrorState.State.Repairing:
                    fillAmount = 1 - mirror.RepairProgress;
                    MirrorAnimator.SetFloat(AnimatorRepairProgressFloatName, mirror.RepairProgress);
                    break;

                default:
                    fillAmount = 0;
                    break;
            }

            ClockOverlay.fillAmount = Mathf.Round(fillAmount * ClockFillSegments) / ClockFillSegments;
        }

        public void SetMirrorState (MirrorState.Mirror mirror)
        {
            this.mirror = mirror;

            MirrorAnimator.runtimeAnimatorController = mirror.AnimatorController;
        }

        public void BreakThisMirror ()
        {
            mirror.Break();
        }
    }
}
