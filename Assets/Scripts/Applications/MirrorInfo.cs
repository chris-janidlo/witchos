using UnityEngine;
using UnityEngine.UI;

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

        public Mirror Mirror;

        void Start ()
        {
            MirrorAnimator.runtimeAnimatorController = Mirror.AnimatorController;
        }

        void Update ()
        {

            MirrorAnimator.SetInteger(AnimatorStateIntegerName, (int) Mirror.CurrentState);

            BreakButton.interactable = Mirror.CurrentState == Mirror.State.Intact;

            Clock.sprite = Mirror.CurrentState == Mirror.State.Dud
                ? BrokenClockSprite
                : RegularClockSprite;

            float fillAmount;

            switch (Mirror.CurrentState)
            {
                case Mirror.State.Broken:
                    fillAmount = Mirror.Timer / Mirror.TimeUntilDud;
                    break;

                case Mirror.State.Repairing:
                    fillAmount = 1 - Mirror.RepairProgress;
                    MirrorAnimator.SetFloat(AnimatorRepairProgressFloatName, Mirror.RepairProgress);
                    break;

                default:
                    fillAmount = 0;
                    break;
            }

            ClockOverlay.fillAmount = Mathf.Round(fillAmount * ClockFillSegments) / ClockFillSegments;
        }

        public void BreakThisMirror ()
        {
            Mirror.Break();
        }
    }
}
