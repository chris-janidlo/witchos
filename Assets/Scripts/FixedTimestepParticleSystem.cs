using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchOS
{
    // for pixel animation feel
    public class FixedTimestepParticleSystem : MonoBehaviour
    {
        public float Timestep;
        public ParticleSystem ParticleSystem;

        float timestepTimer;

        void Update ()
        {
            if (!ParticleSystem.emission.enabled && ParticleSystem.particleCount == 0) return;

            timestepTimer += Time.deltaTime;

            if (timestepTimer >= Timestep)
            {
                ParticleSystem.Simulate(timestepTimer, true, false);
                timestepTimer = 0;
            }
        }
    }
}
