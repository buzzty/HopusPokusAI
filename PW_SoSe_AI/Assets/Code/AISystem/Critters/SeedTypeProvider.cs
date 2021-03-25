using Core.Utility;
using UnityEngine;

namespace AISystem.Critters
{
	/// <summary>
	/// 	Provides a seed type to a game obejct.
	/// </summary>
	public class SeedTypeProvider : CachedMonoBehaviour
	{
		[SerializeField] private SeedType _type = SeedType.ToothyTerror;
		public SeedType Type => _type;
	}
}