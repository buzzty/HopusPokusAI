using System;
using UnityEngine;

public class AnimationEventForwarder : MonoBehaviour
{
	// This is a so called "event" - you can use it to subscribe to it and once the event gets raised, you can execute code that happens when he event occurs.
	// See spitAttackState for reference
	public static event Action OnSpit;
	
	// Gets called by an animation even inside of the Spit Animation from potato boss
	private void Spit()
	{
		// This raises the event. ? does the same as if (OnSpit != null) and is just a shorter way of writing it ("null propagation")
		// anything subbed to OnSpit will get called by using invoke! 
		OnSpit?.Invoke();
	}
}