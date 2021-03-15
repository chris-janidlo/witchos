using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class MailIcon : MonoBehaviour
    {
        public Sprite OnSprite, OffSprite;
        public float FlashTime;
        public DesktopIcon Icon;

        IEnumerator Start ()
        {
            while (true)
            {
                if (MailState.Instance.UnreadMessageCount == 0)
                {
                    Icon.Icon = OffSprite;
                    yield return null;
                }
                else
                {
                    Icon.Icon = OnSprite;
                    yield return new WaitForSeconds(FlashTime);
                    Icon.Icon = OffSprite;
                    yield return new WaitForSeconds(FlashTime);
                }
            }
        }
    }
}
