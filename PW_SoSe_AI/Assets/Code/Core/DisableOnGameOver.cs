using CharacterSystem;
using Core.Utility;

namespace Core
{
	/// <summary>
	/// 	Listens to the <see cref="PlayerDamageable.PlayerDeath"/> event and disables the gameobejct it is attached to.
	/// </summary>
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