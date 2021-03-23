using System;
using AISystem.CagneyCarnation;
using UnityEngine;

namespace AISystem
{
	public class EnemyBrain : MonoBehaviour
	{
		[SerializeField] private EnemyPhaseFSM _phaseFSM = default;
		[SerializeField] private Enemy _enemy;

		private void Start()
		{
			_phaseFSM.Init(_enemy);
		}

		private void Update()
		{
			_phaseFSM.Tick(_enemy);
		}
	}
}