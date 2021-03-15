using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    public class MirrorParticleOrchestrator : MonoBehaviour
    {
        public ParticleSystem MainSystem, TrailSystem, BurstSystem;

        public void StartParticlesWithBurst ()
        {
            setMainPrewarm(false);
            MainSystem.Play();
            BurstSystem.Play();
            TrailSystem.Play();
        }

        public void StartParticlesWithoutBurst ()
        {
            setMainPrewarm(true);
            MainSystem.Play();
            TrailSystem.Play();
        }

        public void StopParticles ()
        {
            MainSystem.Stop();
            TrailSystem.Stop();
        }

        void setMainPrewarm (bool value)
        {
            var mainMain = MainSystem.main;
            mainMain.prewarm = value;
        }
    }
}
