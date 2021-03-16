using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using crass;

namespace WitchOS
{
    public class Alert : Singleton<Alert>, IPointerClickHandler
    {
        public float SecondsToDisplayPerWord;
        public TransitionableFloat FadeoutTransition;

        public TextMeshProUGUI Text;
        public CanvasGroup Group;

        public TimeState TimeState;

        LinkedList<string> messageQueue = new LinkedList<string>();

        float showTimer = -1;
        bool faded = true;

        void Awake ()
        {
            SingletonOverwriteInstance(this);
            TimeState.DayEnded.AddListener(ClearMessages);
        }

        void Start ()
        {
            FadeoutTransition.AttachMonoBehaviour(this);
            Group.alpha = 0;
        }

        void Update ()
        {
            // enable clicking away when message is fully visible, while not blocking interaction with anything below the message if it's transparent at all
            Group.blocksRaycasts = Group.alpha == 1;

            if (messageQueue.Count > 0 && faded)
            {
                faded = false;

                string message = messageQueue.First.Value;
                messageQueue.RemoveFirst();

                Text.text = message;
                Group.alpha = 1;

                showTimer = message.Split(' ').Length * SecondsToDisplayPerWord;
            }

            if (showTimer > 0)
            {
                showTimer -= Time.deltaTime;
            }
            else if (!faded)
            {
                if (Group.alpha == 1 && !FadeoutTransition.Transitioning)
                    FadeoutTransition.FlashFromTo(1, 0);

                Group.alpha = FadeoutTransition.Value;

                faded = Group.alpha == 0;
            }
        }

        public void OnPointerClick (PointerEventData eventData)
        {
            hideImmediately();
        }

        public void ClearMessages ()
        {
            // for end of day
            showTimer = 0;
            Group.alpha = 0;
            faded = true;

            messageQueue.Clear();
        }

        public void ShowMessage (string message)
        {
            messageQueue.AddLast(message);
        }

        public void ShowMessageImmediately (string message)
        {
            hideImmediately();
            messageQueue.AddFirst(message);
        }

#if UNITY_EDITOR
        [ContextMenu("Test Message (wait for every other pending message to show)")]
        public void TriggerTestMessage ()
        {
            ShowMessage("This is a test");
        }

        [ContextMenu("Test Message (show immediately, dismissing the current message)")]
        public void TriggerTestMessageNext ()
        {
            ShowMessageImmediately("This is a test");
        }
#endif // UNITY_EDITOR

        void hideImmediately ()
        {
            showTimer = Mathf.Min(showTimer, 0);
        }
    }
}
