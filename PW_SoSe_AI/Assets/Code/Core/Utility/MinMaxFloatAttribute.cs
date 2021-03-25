using System;

namespace Core.Utility
{
	/// <summary>
	///  Attribute to work in junction with the <see cref="MinMaxFloat"/>
	///  Provides a minimum and a maximum value to define ranges.
	/// </summary>
	public class MinMaxFloatAttribute : Attribute
	{
		public MinMaxFloatAttribute(float min, float max)
		{
			Min = min;
			Max = max;
		}
		public float Min { get; }
		public float Max { get; }
	}
}