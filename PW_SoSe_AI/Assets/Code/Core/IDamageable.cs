namespace Core
{
	/// <summary>
	/// 	interface for all Damageable entities. Provides common functionality like "OnHit" and whether its the player or not.
	/// </summary>
	public interface IDamageable
	{
		bool IsPlayer
		{
			get;
		}

		bool IsInvincible
		{
			get;
			set;
		}

		bool OnHit(int damage);
	}
}