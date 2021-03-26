using System;
using UnityEngine;

public class IdleState : MonoBehaviour, IPotatoBossState
{
	private float _currentTimeInState;
	
	public IPotatoBossState Handle(PotatoBoss boss)
	{
		// we died, go to death state
		if (boss.IsDead)
		{
			return boss.Death;
		}
		
		// we count up the itme in the current state, to check against cooldowns
		_currentTimeInState += Time.deltaTime;
		
		// lets check for transitions to other states:
		if (boss.Spit.SpitCooldown <= _currentTimeInState)
		{
			// cooldown has passed - lets go spit!
			return boss.Spit;
		}

		// we stay in idle
		return this;
	}

	public void StateEnter(PotatoBoss boss)
	{
		// reset the time for cooldown
		_currentTimeInState = 0;
	}

	public void StateExit(PotatoBoss boss)
	{
	}
}