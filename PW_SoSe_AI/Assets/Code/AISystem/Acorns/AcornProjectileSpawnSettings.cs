using Core.Utility;
using UnityEngine;

namespace AISystem.Acorns
{
	[CreateAssetMenu(menuName = "Cuphead/Bosses/CagneyCarnation/Projectiles/AcornSettings", fileName = "AcornSettings", order = 0)]
	public class AcornProjectileSpawnSettings : ScriptableObject
	{
		[SerializeField] [MinMaxFloat(0.5f, 2.0f)]
		private MinMaxFloat _minMaxStartDelay = new MinMaxFloat(0.5f, 2.0f);
		[SerializeField] [Range(0.25f, 0.5f)]
		private float _delayBetweenAcorns = 0.25f;
		[SerializeField] private float _distanceBetweenAcorns = 0.5f;
		
		public MinMaxFloat MinMaxStartDelay => _minMaxStartDelay;
		public float DelayBetweenAcorns => _delayBetweenAcorns;
		public float DistanceBetweenAcorns => _distanceBetweenAcorns;
	}
}