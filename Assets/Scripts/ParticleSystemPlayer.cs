using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemPlayer : MonoBehaviour
{
    public ParticleSystem ParticleSystem;

    public void Play ()
    {
        ParticleSystem.Stop();
        ParticleSystem.Play();
    }
}
