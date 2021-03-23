using CharacterSystem;
using Core.Utility;

namespace Core
{
	public class DisableOnGameOver : CachedMonoBehaviour
	{
		private void Start()
		{
			PlayerDamageable.PlayerDeath += OnPlayerDeath;
		}

		private void OnDestroy()
		{
			PlayerDamageable.PlayerDeath -= OnPlayerDeath;
		}

		private void OnPlayerDeath()
		{
			gameObject.SetActive(false);
		}
	}
}