using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

namespace WitchOS
{
    public class Minimizer : MonoBehaviour
    {
        public bool Minimized { get; private set; }

        public RectTransform MinimizeTarget;
        public TransitionableFloat Transition;
        public bool MinimizedAtStart;

        Vector2 unminimizedLocation;

        void Awake ()
        {
            Transition.AttachMonoBehaviour(this);
            Transition.Value = MinimizedAtStart ? 0 : 1;
        }

        void Start ()
        {
            unminimizedLocation = transform.position;
            Minimized = MinimizedAtStart;
        }

        // Update is called once per frame
        void Update ()
        {
            if (MinimizeTarget != null && (Transition.Transitioning || edgeCheck()))
                doMinimizeAnimation();

            bool fullyUnMinimized = Transition.Value == 1;

            if (fullyUnMinimized) unminimizedLocation = transform.position;

            var ctp = GetComponent<ClampToParent>();
            if (ctp != null) ctp.enabled = fullyUnMinimized;
        }

        public void Minimize ()
        {
            if (Minimized) return;

            Transition.StartTransitionTo(0);

            Minimized = true;
        }

        public void UnMinimize ()
        {
            if (!Minimized) return;

            Transition.StartTransitionTo(1);

            Minimized = false;
        }

        bool edgeCheck ()
        {
            // for the case where the transition has finished, but the scale is outdated
            return
                (Transition.Value == 0 && transform.localScale.x != 0) ||
                (Transition.Value == 1 && transform.localScale.x != 1);
        }

        void doMinimizeAnimation ()
        {
            float scalar = Transition.Value;

            transform.localScale = Vector2.one * scalar;
            transform.position = Vector2.Lerp
            (
                MinimizeTarget.transform.position,
                unminimizedLocation,
                scalar
            );
        }
    }
}
