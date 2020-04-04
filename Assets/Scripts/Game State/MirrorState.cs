using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class MirrorState : Singleton<MirrorState>
{
	public enum State
	{
		Intact, Broken, Depleted
	}

	[Serializable]
	public class Mirror
	{
		public const float TIMER_TICKS_PER_SECOND = 1;
		public State State;
		public float Timer;

		public void Break ()
		{
			if (State != State.Intact)
			{
				throw new ArgumentException("can't break an already broken mirror");
			}

			State = State.Broken;
		}

		public void ConsumeMagic ()
		{
			if (State != State.Broken)
			{
				throw new ArgumentException("can't consume a mirror that's intact or depleted");
			}

			State = State.Depleted;
			Timer *= 2;
		}

		public void Tick ()
		{
			switch (State)
			{
				case State.Intact: return;

				case State.Broken:
					Timer += TIMER_TICKS_PER_SECOND * Time.deltaTime;
					return;

				case State.Depleted:
					Timer -= TIMER_TICKS_PER_SECOND * Time.deltaTime;
					if (Timer <= 0) State = State.Intact;
					return;
			}
		}
	}

	public List<Mirror> Mirrors;

	void Awake ()
	{
		SingletonOverwriteInstance(this);
	}

	void Update ()
	{
		foreach (var mirror in Mirrors)
		{
			mirror.Tick();
		}
	}

	public bool TryConsumeMagic ()
	{
		foreach (var mirror in Mirrors.OrderByDescending(m => m.Timer))
		{
			if (mirror.State == State.Broken)
			{
				mirror.ConsumeMagic();
				return true;
			}
		}

		return false;
	}

	public int NumberIntact ()
	{
		return Mirrors.Where(m => m.State == State.Intact).Count();
	}

	public int NumberBroken ()
	{
		return Mirrors.Where(m => m.State == State.Broken).Count();
	}
}
