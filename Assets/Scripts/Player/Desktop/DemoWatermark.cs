using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using crass;

namespace WitchOS
{
    public class DemoWatermark : MonoBehaviour
    {
        public TransitionableFloat XOffset;
        public Vector2 Origin;
        public float OffscreenDistance, OffscreenTime, OnscreenTime;

        public ParticleSystem TrailParticles;

#if UNITY_WEBGL
        IEnumerator Start ()
        {
            transform.localPosition = Origin;

            XOffset.AttachMonoBehaviour(this);

            while (true)
            {
                yield return new WaitForSeconds(OnscreenTime);
                yield return moveRoutine(Origin.x + OffscreenDistance);

                yield return new WaitForSeconds(OffscreenTime);

                TrailParticles.Stop();
                XOffset.Value = Origin.x - OffscreenDistance;
                yield return null;

                TrailParticles.Play();
                yield return moveRoutine(Origin.x);
            }
        }

        IEnumerator moveRoutine (float targetX)
        {
            XOffset.StartTransitionTo(targetX);
            while (XOffset.Transitioning)
            {
                transform.localPosition = new Vector2(Mathf.Round(XOffset.Value), Origin.y);
                yield return null;
            }
        }
#else // if !UNITY_WEBGL
        void Start ()
        {
            throw new InvalidOperationException("should not instantiate a demo watermark object when not in WebGL");
        }
#endif // !UNITY_WEBGL
    }
}
