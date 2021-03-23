namespace Core
{
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