using Core.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AISystem.CagneyCarnation.States
{
	public class GatlingFX : CachedMonoBehaviour
	{
		[SerializeField] private GameObject _normalFX = default;
		[SerializeField] private GameObject _parryableFX = default;
		[SerializeField] private EnemyAnimationEventReceiver _receiver;

		private Animator[] _fx = new Animator[10];
		private int _index = 0;
		
		protected override void Awake()
		{
			base.Awake();

			for (int i = 0; i < _fx.Length; i++)
			{
				_fx[i] = Instantiate(Random.value < 0.7f ? _normalFX : _parryableFX, transform).GetComponent<Animator>();
				_fx[i].gameObject.SetActive(false);
			}
		}

		private void Start()
		{
			_receiver.OnSpawnGatlingProjectile += SpawnFX;
		}

		private void OnDestroy()
		{
			_receiver.OnSpawnGatlingProjectile -= SpawnFX;
		}

		private void SpawnFX()
		{
			_fx[_index].gameObject.SetActive(true);
			_fx[_index].SetTrigger("Spawn");

			_index++;
			_index %= _fx.Length;
		}
	}
}