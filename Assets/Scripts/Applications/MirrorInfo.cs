using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WitchOS
{
    public class MirrorInfo : MonoBehaviour
    {
        public Animator IconAnimator;
        public string AnimatorStateIntegerName;

        public TextMeshProUGUI StateLabel;

        public Button BreakButton;
        public TextMeshProUGUI BreakButtonLabel;

        MirrorState.Mirror mirror;

        void Update ()
        {
            if (mirror == null) return;

            BreakButton.interactable = mirror.State == MirrorState.State.Intact;
            BreakButtonLabel.text = mirror.State == MirrorState.State.Intact ? "Break" : "";

            int time = (int) mirror.Timer;

            switch (mirror.State)
            {
                case MirrorState.State.Intact:
                    StateLabel.text = "Intact";
                    break;

                case MirrorState.State.Broken:
                    StateLabel.text = $"Broken {time} second{(time != 1 ? "s" : "")} ago";
                    break;

                case MirrorState.State.Depleted:
                    StateLabel.text = $"Depleted.\n{time} second{(time != 1 ? "s" : "")} until repair";
                    break;
            }

            IconAnimator.SetInteger(AnimatorStateIntegerName, (int) mirror.State);
        }

        public void SetMirrorState (MirrorState.Mirror mirror)
        {
            this.mirror = mirror;

            BreakButton.onClick.AddListener(mirror.Break);
        }
    }
}
