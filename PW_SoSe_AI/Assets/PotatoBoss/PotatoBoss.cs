using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoBoss : MonoBehaviour
{
	// State references: States in our example are monobehaviours (they could also be plain c# classes)
	[SerializeField] private IdleState _idle;
	[SerializeField] private SpitAttackState _spit;
	[SerializeField] private DeathState _death;
	// value to tweak bosses max health
	[SerializeField] private int _maxHealth;

	// private variables - animator, currentState etc
	private Animator _animator;
	private IPotatoBossState _currentState;
	private bool _isCurrentAnimationDone;
	private int _currentHealth;

	// public pproperties - used to access the data from this class in another class that has a reference to it.
	// using the "=>" means the same as public bool IsDead {get {return _currentHealth <= 0} } and is just a shorter way of writing it
	public bool IsDead => _currentHealth <= 0;
	public SpitAttackState Spit => _spit;
	public IdleState Idle => _idle;
	public DeathState Death => _death;
	public Animator Animator => _animator;
	// this has get and set access, because we need to reset it
	public bool IsCurrentAnimationDone { get => _isCurrentAnimationDone; set => _isCurrentAnimationDone = value; }

	private void Awake()
	{
		// fetching the animator
		_animator = GetComponent<Animator>();
		
		// initialize starting state -> idle and enter the state 
		_currentState = _idle;
		_currentState.StateEnter(this);

		// set health to maximum health
		_currentHealth = _maxHealth;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			_currentHealth--;
		}
		
		// finite state machine pattern: poll _currentState.Handle() method and pass a reference of ourself in
		// Handle will return a IPotateBossState. if it is different than the _currentState, transition.
		IPotatoBossState nextState = _currentState.Handle(this);
		if (nextState != _currentState)
		{
			_currentState.StateExit(this);
			_currentState = nextState;
			_currentState.StateEnter(this);
		}
	}

	// called by animation event in "Spit" of the boss
	private void CurrentAnimationDone()
	{
		_isCurrentAnimationDone = true;
	}
}