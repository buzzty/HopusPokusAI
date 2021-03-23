using Core.Utility;
using UnityEngine;

namespace AISystem.Critters
{
	public class SeedTypeProvider : CachedMonoBehaviour
	{
		[SerializeField] private SeedType _type = SeedType.ToothyTerror;
		public SeedType Type => _type;
	}
}