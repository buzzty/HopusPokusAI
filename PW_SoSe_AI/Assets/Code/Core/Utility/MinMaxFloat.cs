using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Core.Utility
{
	/// <summary>
	/// Class used to summarize use cases where we need a min and a max float
	/// </summary>
	[Serializable]
	public class MinMaxFloat
	{
		public float MinValue;
		public float MaxValue;

		public MinMaxFloat(float min, float max)
		{
			MinValue = min;
			MaxValue = max;
		}

		public float GetRandomBetween()
		{
			return Random.Range(MinValue, MaxValue);
		}
	}

	public static class FloatExtensions
	{
		/// <summary>
		/// 	Extension method that checks whether a float is in the given range or not.
		/// </summary>
		/// <param name="f">float to check</param>
		/// <param name="range">range to compare against</param>
		/// <returns>True if the value is within the range</returns>
		public static bool IsInRange(this float f, MinMaxFloat range)
		{
			return (range.MaxValue >= f) && (f >= range.MinValue);
		}
	}

	public static class ListExtensions
	{
		public static T PickRandom<T>(this List<T> list)
		{
			return list[Random.Range(0, list.Count)];
		}

		/// <summary>
		/// 	Randomly pick an element from a list based on a random roll. Considers weight of the elements.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="roll"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T PickRandomWeighted<T>(this List<T> list, float roll) where T : IWeightable
		{
			float totalSum = list.Sum(l => l.Weight);
			
			foreach (T value in list)
			{
				float valueWeight = value.Weight / totalSum;
				if (roll <= valueWeight)
				{
					return value;
				}

				roll -= valueWeight;
			}

			return default;
		}   
		
		/// <summary>
		/// 	Shuffles the order of elements in a list.
		/// </summary>
		/// <param name="ts"></param>
		/// <typeparam name="T"></typeparam>
		public static void Shuffle<T>(this IList<T> ts) 
		{
			int count = ts.Count;
			int last = count - 1;
			for (int i = 0; i < last; ++i) {
				int r = Random.Range(i, count);
				T tmp = ts[i];
				ts[i] = ts[r];
				ts[r] = tmp;
			}
		}
	}

	/// <summary>
	/// 	Interface to define weight for randomness on a class.
	/// </summary>
	public interface IWeightable
	{
		int Weight { get; }
	}
}