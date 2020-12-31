using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using crass;

namespace WitchOS
{
    public class DemoWatermark : MonoBehaviour
    {
        public string WatermarkText;
        public TransitionableFloat XOffset;
        public Vector2 Origin;
        public float OffscreenDistance, OffscreenTime, OnscreenTime;

        public TextMeshProUGUI WatermarkTextObject;
        public ParticleSystem WatermarkTrailParticles;

#if UNITY_WEBGL
        IEnumerator Start ()
        {
            WatermarkTextObject.text = WatermarkText;
            WatermarkTextObject.transform.localPosition = Origin;

            XOffset.AttachMonoBehaviour(this);

            while (true)
            {
                yield return new WaitForSeconds(OnscreenTime);
                yield return moveRoutine(Origin.x + OffscreenDistance);
                
                yield return new WaitForSeconds(OffscreenTime);

                WatermarkTrailParticles.Stop();
                XOffset.Value = Origin.x - OffscreenDistance;
                yield return null;

                WatermarkTrailParticles.Play();
                yield return moveRoutine(Origin.x);
            }
        }

        IEnumerator moveRoutine (float targetX)
        {
            XOffset.StartTransitionTo(targetX);
            while (XOffset.Transitioning)
            {
                WatermarkTextObject.transform.localPosition = new Vector2(Mathf.Round(XOffset.Value), Origin.y);
                yield return null;
            }
        }
#endif // UNITY_WEBGL
    }
}
