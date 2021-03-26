using UnityEngine;

public class SpitAttackState : MonoBehaviour, IPotatoBossState
{
	// cooldown of the spit attack
	[SerializeField] private float _spitCooldown;
	// projectile to shoot
	[SerializeField] private SpitProjectile _projectile;
	// where to instantiate the projectile
	[SerializeField] private Transform _projectileSpawnPosition;
	
	// create a public getter for acces of cooldown if we need it else where
	public float SpitCooldown => _spitCooldown;
	
	public IPotatoBossState Handle(PotatoBoss boss)
	{
		// were dead -> death state
		if (boss.IsDead)
		{
			return boss.Death;
		}
		
		// animation is done -> idle
		if (boss.IsCurrentAnimationDone)
		{
			// make sure to reset the flag here or otherwise it will be true next time we enter the state - could also be done in StateExit! (doesnt matter too much)
			boss.IsCurrentAnimationDone = false;
			return boss.Idle;
		}
		
		// stay in spit state
		return this;
	}

	public void StateEnter(PotatoBoss boss)
	{
		// Subscribing to an event - we need to unregister from it at some point!
		AnimationEventForwarder.OnSpit += SpawnProjectile;
		
		// set animator trigger - has to be named the same as our parameter in animator!
		boss.Animator.SetTrigger("Spit");
	}
	
	private void SpawnProjectile()
	{
		// Spawn the projectile, using the prefab and placing it as a child of _projectileSpawnPosition. 
		Instantiate(_projectile, _projectileSpawnPosition);
	}

	public void StateExit(PotatoBoss boss)
	{
		// unsubscribing from an event
		AnimationEventForwarder.OnSpit -= SpawnProjectile;
	}
}