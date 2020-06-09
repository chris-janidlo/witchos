using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using crass;

namespace WitchOS
{
public class MagicSource : Singleton<MagicSource>
{
    [Serializable]
    public enum State
    {
        Off, On, Depleted
    }

    public bool Off => CurrentState == State.Off;
    public bool On => CurrentState == State.On;
    public bool Depleted => CurrentState == State.Depleted;

    public float RemainingOnTime { get; private set; }

    public float MaxOnTimeSeconds;

    [SerializeField]
    State _currentState;
    public State CurrentState
    {
        get => _currentState;
        private set => _currentState = value;
    }

    public UnityEvent PowerTurnedOn, PowerTurnedDepleted;

    void Awake ()
    {
        SingletonOverwriteInstance(this);
    }

    public void StartDay ()
    {
        CurrentState = State.Off;
    }

    public void EndDay ()
    {
        StopAllCoroutines();
    }

    public void TurnOn ()
    {
        if (CurrentState != State.Off) return;

        StartCoroutine(onRoutine());
    }

    IEnumerator onRoutine ()
    {
        CurrentState = State.On;
        PowerTurnedOn.Invoke();

        RemainingOnTime = MaxOnTimeSeconds;
        while (RemainingOnTime > 0)
        {
            RemainingOnTime -= Time.deltaTime;
            yield return null;
        }
        RemainingOnTime = 0;

        CurrentState = State.Depleted;
        PowerTurnedDepleted.Invoke();
    }
}
}
