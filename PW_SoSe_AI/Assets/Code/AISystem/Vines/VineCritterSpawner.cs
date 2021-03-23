using System;
using AISystem.Critters;
using Core.Utility;
using UnityEngine;

namespace AISystem.Vines
{
	public class VineCritterSpawner : CachedMonoBehaviour
	{
		[SerializeField] private GameObject _toothyTerrorFloaterPrefab = default;
		[SerializeField] private GameObject _toothyTerrorPrefab = default;
		[SerializeField] private Transform _spawnPoint = default;

		private SeedType _type;
		
		private void SpawnCritter()
		{
			switch (_type)
			{
				case SeedType.ToothyTerror:
					Instantiate(_toothyTerrorPrefab, _spawnPoint.position, Quaternion.identity);
					break;
				case SeedType.ToothyTerrorFloater:
					Instantiate(_toothyTerrorFloaterPrefab, _spawnPoint.position, Quaternion.identity);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(_type), _type, null);
			}

			Destroy(gameObject, 0.25f);
		}
		
		public void SetVineSeedType(SeedType type)
		{
			_type = type;
		}
	}
}