using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class Minimizer : MonoBehaviour
{
    public bool Minimized { get; private set; }

    public RectTransform MinimizeTarget;
    public TransitionableFloat Transition;

    Vector2 unminimizedLocation;

    void Start ()
    {
        Transition.AttachMonoBehaviour(this);
        Transition.Value = 1;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Transition.Transitioning && MinimizeTarget != null)
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
