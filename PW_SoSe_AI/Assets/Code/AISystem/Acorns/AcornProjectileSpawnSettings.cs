using Core.Utility;
using UnityEngine;

namespace AISystem.Acorns
{
	/// <summary>
	/// 	Scriptable Object to determine spawn settings of the acorns.
	/// </summary>
	[CreateAssetMenu(menuName = "Cuphead/Bosses/CagneyCarnation/Projectiles/AcornSettings", fileName = "AcornSettings", order = 0)]
	public class AcornProjectileSpawnSettings : ScriptableObject
	{
		// time in seconds to delay the acorn from flying - random value between lower and upper boundary
		[SerializeField] [MinMaxFloat(0.5f, 2.0f)]
		private MinMaxFloat _minMaxStartDelay = new MinMaxFloat(0.5f, 2.0f);
		// if one acorn spawned, it takes at _delayBetweenAcorns seconds to spawn another one
		[SerializeField] [Range(0.25f, 0.5f)]
		private float _delayBetweenAcorns = 0.25f;
		// how far theyre apart from another
		[SerializeField] private float _distanceBetweenAcorns = 0.5f;
		
		public MinMaxFloat MinMaxStartDelay => _minMaxStartDelay;
		public float DelayBetweenAcorns => _delayBetweenAcorns;
		public float DistanceBetweenAcorns => _distanceBetweenAcorns;
	}
}