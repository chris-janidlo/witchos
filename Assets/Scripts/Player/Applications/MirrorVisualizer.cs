using UnityEngine;
using UnityEngine.UI;

namespace WitchOS
{
    public class MirrorVisualizer : MonoBehaviour
    {
        public Animator MirrorAnimator;
        public string AnimatorStateIntegerName, AnimatorRepairProgressFloatName;

        public Image Clock, ClockOverlay;
        public Sprite RegularClockSprite, BrokenClockSprite;
        public int ClockFillSegments;

        public ParticleSystem ParticleSystem;
        public AnimationCurve ParticleMotionIntensityByDistanceFromSweetspot;
        public Vector2 ParticleVelocityMultiplierRange, ParticleLifetimeRange, ParticleSpawnRateRange;

        public Button BreakButton;

        public Mirror Mirror;

        Mirror.State previousState;

        void Start ()
        {
            MirrorAnimator.runtimeAnimatorController = Mirror.AnimatorController;

            animateMirror(); // get animator in correct state immediately so that it doesn't play the "break" animation if the app loads when the mirror is not intact
        }

        void Update ()
        {
            animateMirror();
            animateClock();
            animateParticles();

            previousState = Mirror.CurrentState;
        }

        public void BreakThisMirror ()
        {
            Mirror.Break();
        }

        void animateMirror ()
        {
            MirrorAnimator.SetInteger(AnimatorStateIntegerName, (int) Mirror.CurrentState);
            BreakButton.interactable = Mirror.CurrentState == Mirror.State.Intact;

            if (Mirror.CurrentState == Mirror.State.Repairing)
            {
                MirrorAnimator.SetFloat(AnimatorRepairProgressFloatName, Mirror.RepairProgress);
            }
        }

        void animateClock ()
        {
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
                    break;

                default:
                    fillAmount = 0;
                    break;
            }

            ClockOverlay.fillAmount = Mathf.Round(fillAmount * ClockFillSegments) / ClockFillSegments;
        }

        void animateParticles ()
        {
            if (Mirror.CurrentState == Mirror.State.Broken)
            {
                setParticleIntensity(Mirror.DistanceFromSweetspot);
            }
            else if (previousState == Mirror.State.Broken)
            {
                makeParticlesDieFaster();
            }
        }

        void setParticleIntensity (float distanceFromSweetspot)
        {
            float visibleIntensity = ParticleMotionIntensityByDistanceFromSweetspot.Evaluate(distanceFromSweetspot);

            var mainModule = ParticleSystem.main;
            var emissionModule = ParticleSystem.emission;
            var velocityModule = ParticleSystem.velocityOverLifetime;

            mainModule.startLifetime = Mathf.Lerp(ParticleLifetimeRange.x, ParticleLifetimeRange.y, visibleIntensity);
            emissionModule.rateOverTimeMultiplier = Mathf.Lerp(ParticleSpawnRateRange.x, ParticleSpawnRateRange.y, visibleIntensity);
            velocityModule.speedModifierMultiplier = Mathf.Lerp(ParticleVelocityMultiplierRange.x, ParticleVelocityMultiplierRange.y, visibleIntensity);
        }

        void makeParticlesDieFaster ()
        {
            var particles = new ParticleSystem.Particle[ParticleSystem.main.maxParticles];
            ParticleSystem.GetParticles(particles);

            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].remainingLifetime /= ParticleLifetimeRange.x;
            }

            ParticleSystem.SetParticles(particles);
        }
    }
}
