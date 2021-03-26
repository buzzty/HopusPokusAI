using System;
using UnityEngine;

public class DeathState : MonoBehaviour, IPotatoBossState
{
	// Time based state: lets have a variable to check how long to stay in here at max
	[SerializeField] private float _maxTimeInState;

	// how long are we in the state already?
	private float _currentTimeInState;
	
	public IPotatoBossState Handle(PotatoBoss boss)
	{
		// increment time in state by Time.deltaTime, which is the time between frames (very small amount)
		_currentTimeInState += Time.deltaTime;
		// Check for state transitions: In your case after the phase 1 death state you could go -> phase2Idle or whatever!
		if (_currentTimeInState >= _maxTimeInState)
		{
			// in your case: return state of next phase! :)
			return boss.Idle;
		}
		
		// death is our final state - lets terminate here!
		return this;
	}

	public void StateEnter(PotatoBoss boss)
	{
		// trigger animation
		boss.Animator.SetTrigger("Death");
		// reset time in state
		_currentTimeInState = 0;
	}

	public void StateExit(PotatoBoss boss)
	{
	}
}